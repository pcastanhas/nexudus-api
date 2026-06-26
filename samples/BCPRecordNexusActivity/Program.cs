using System.Globalization;
using BcpRecordNexusActivity.Configuration;
using BcpRecordNexusActivity.Io;
using BcpRecordNexusActivity.Journal;
using BcpRecordNexusActivity.Mapping;
using BcpRecordNexusActivity.Nexudus;
using Nexudus;

namespace BcpRecordNexusActivity;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        // Config load is special: if it fails we have no recipients/SMTP to email, so just report and exit.
        AppSettings settings;
        var configPath = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        try
        {
            settings = AppSettingsLoader.Load(configPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Configuration error: {ex.Message}");
            return 2;
        }

        var emailSender = new SmtpEmailSender(settings.Smtp, settings.Notifications.Recipients);
        var workbookWriter = new XlsxWorkbookWriter();
        var outputDir = Directory.GetCurrentDirectory();
        var stamp = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var period = settings.Run.PostingPeriod;

        try
        {
            using var client = CreateClient(settings.Nexudus);
            var dataSource = new NexudusDataSource(client);

            Console.WriteLine($"Pulling Nexudus activity {settings.Run.FromDate:yyyy-MM-dd}..{settings.Run.ToDate:yyyy-MM-dd}");
            var data = await dataSource.LoadAsync(settings.Run.FromDate, settings.Run.ToDate).ConfigureAwait(false);
            Console.WriteLine($"  invoices: {data.Invoices.Count}, payments: {data.Payments.Count}");

            var resolver = new MappingResolver(settings.EntityMappings, settings.GlAccountMappings);
            var builder = new JournalBuilder(resolver);
            var result = builder.Build(data, period, DateTime.Now);

            if (result.HasErrors)
                return await HandleErrorsAsync(result, workbookWriter, emailSender, outputDir, period, stamp).ConfigureAwait(false);

            var allRows = result.AllRows.ToList();
            if (allRows.Count == 0)
            {
                Console.WriteLine("Nothing to post for this range.");
                await emailSender.SendAsync(
                    $"Nexudus -> MRI: nothing to post for {period}",
                    $"No invoices or payments were found between {settings.Run.FromDate:yyyy-MM-dd} and {settings.Run.ToDate:yyyy-MM-dd}. Nothing was posted.",
                    attachmentPath: null).ConfigureAwait(false);
                return 0;
            }

            // Post first (single transaction), then produce the workbook mirroring what posted.
            var journalWriter = new SqlJournalWriter(settings.Journal.ConnectionString);
            Console.WriteLine($"Posting {allRows.Count} JOURNAL rows...");
            var inserted = await journalWriter.WriteAsync(allRows).ConfigureAwait(false);
            Console.WriteLine($"  committed {inserted} rows.");

            var workbookPath = Path.Combine(outputDir, $"NexusMri_Journals_{period}_{stamp}.xlsx");
            workbookWriter.WriteJournalWorkbook(workbookPath, result.InvoiceRows, result.PaymentRows);
            Console.WriteLine($"  workbook: {workbookPath}");

            await emailSender.SendAsync(
                $"Nexudus -> MRI: posted {inserted} JOURNAL rows for {period}",
                BuildSuccessBody(settings, result, inserted),
                workbookPath).ConfigureAwait(false);

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Run failed: {ex}");
            await TryEmailFailureAsync(emailSender,
                $"Nexudus -> MRI: run FAILED for {period}",
                $"The run failed and nothing was posted.{Environment.NewLine}{Environment.NewLine}{ex}").ConfigureAwait(false);
            return 1;
        }
    }

    private static NexudusClient CreateClient(NexudusSettings nx)
    {
        var username = First(nx.Username, Environment.GetEnvironmentVariable("NEXUDUS_USERNAME"));
        var password = First(nx.Password, Environment.GetEnvironmentVariable("NEXUDUS_PASSWORD"));
        var totp = First(nx.Totp, Environment.GetEnvironmentVariable("NEXUDUS_TOTP"));
        var baseUrl = First(nx.BaseUrl, Environment.GetEnvironmentVariable("NEXUDUS_BASE_URL"));

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException(
                "Nexudus credentials are missing. Set them in appsettings.json (Nexudus section) or the "
                + "NEXUDUS_USERNAME / NEXUDUS_PASSWORD environment variables.");

        return NexudusClient.WithPassword(username!, password!,
            string.IsNullOrWhiteSpace(totp) ? null : totp,
            string.IsNullOrWhiteSpace(baseUrl) ? null : baseUrl);
    }

    private static string? First(string? a, string? b) => string.IsNullOrWhiteSpace(a) ? b : a;

    private static async Task<int> HandleErrorsAsync(BuildResult result, IWorkbookWriter workbookWriter,
        IEmailSender emailSender, string outputDir, string period, string stamp)
    {
        var path = Path.Combine(outputDir, $"NexusMri_Errors_{period}_{stamp}.xlsx");
        workbookWriter.WriteErrorWorkbook(path, result.Errors);
        Console.Error.WriteLine($"{result.Errors.Count} mapping/validation error(s); nothing posted. See {path}");

        await emailSender.SendAsync(
            $"Nexudus -> MRI: {result.Errors.Count} error(s) for {period} - NOTHING POSTED",
            $"The run found {result.Errors.Count} mapping/validation problem(s). No journals were posted and "
            + "no entries were written to the database. Details are in the attached workbook.",
            path).ConfigureAwait(false);

        return 1;
    }

    private static async Task TryEmailFailureAsync(IEmailSender sender, string subject, string body)
    {
        try { await sender.SendAsync(subject, body, attachmentPath: null).ConfigureAwait(false); }
        catch (Exception ex) { Console.Error.WriteLine($"(Also failed to send failure email: {ex.Message})"); }
    }

    private static string BuildSuccessBody(AppSettings settings, BuildResult result, int inserted)
    {
        var invoiceRefs = result.InvoiceRows.Select(r => r.Ref).Distinct().Count();
        var paymentRefs = result.PaymentRows.Select(r => r.Ref).Distinct().Count();
        return string.Join(Environment.NewLine, new[]
        {
            $"Range: {settings.Run.FromDate:yyyy-MM-dd}..{settings.Run.ToDate:yyyy-MM-dd}",
            $"Period: {settings.Run.PostingPeriod}",
            "",
            $"Invoice journals: {invoiceRefs} (rows: {result.InvoiceRows.Count})",
            $"Payment journals: {paymentRefs} (rows: {result.PaymentRows.Count})",
            $"Total JOURNAL rows posted: {inserted}",
            "",
            "The attached workbook mirrors the rows posted to JOURNAL."
        });
    }
}
