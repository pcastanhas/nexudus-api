using System.Globalization;
using Nexudus;
using Nexudus.Billing.CoworkerInvoices.API;
using Nexudus.Billing.CoworkerInvoices.Models;
using Nexudus.Billing.CoworkerLedgerEntries.API;
using Nexudus.Billing.CoworkerLedgerEntries.Models;

// ---------------------------------------------------------------------------
// Demo: list the customer invoices and the payments received in a date range.
//
// "Invoices"  -> CoworkerInvoice    (invoices issued to your customers)
// "Payments"  -> CoworkerLedgerEntry credit entries (money received into the
//                customer ledger). In a ledger, payments are the credit side.
//
// The operator-level Invoice / LedgerEntry endpoints (bills Nexudus issues to
// the operator) follow the exact same pattern if that's what you need instead.
//
// Authentication is username/password: NexudusClient performs the OAuth
// password grant and refreshes the bearer token for you.
//
// Usage:
//   InvoicesAndPayments [fromDate] [toDate]      (dates as yyyy-MM-dd)
// Credentials & options come from environment variables:
//   NEXUDUS_USERNAME   (required)
//   NEXUDUS_PASSWORD   (required)
//   NEXUDUS_TOTP       (optional, if the account has 2FA enabled)
//   NEXUDUS_BASE_URL   (optional, defaults to https://spaces.nexudus.com/api)
//   NEXUDUS_BUSINESS_ID(optional, restrict to a single location)
// ---------------------------------------------------------------------------

string? username = Environment.GetEnvironmentVariable("NEXUDUS_USERNAME");
string? password = Environment.GetEnvironmentVariable("NEXUDUS_PASSWORD");
string? totp = Environment.GetEnvironmentVariable("NEXUDUS_TOTP");
string? baseUrl = Environment.GetEnvironmentVariable("NEXUDUS_BASE_URL");
long? businessId = long.TryParse(Environment.GetEnvironmentVariable("NEXUDUS_BUSINESS_ID"), out var b) ? b : null;

if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
{
    Console.Error.WriteLine("Set NEXUDUS_USERNAME and NEXUDUS_PASSWORD environment variables first.");
    Console.Error.WriteLine("Usage: InvoicesAndPayments [fromDate yyyy-MM-dd] [toDate yyyy-MM-dd]");
    return 1;
}

// Date range: from CLI args, else the last 30 days.
var to = ParseDate(args.ElementAtOrDefault(1)) ?? DateTimeOffset.UtcNow;
var from = ParseDate(args.ElementAtOrDefault(0)) ?? to.AddDays(-30);
if (from > to)
    (from, to) = (to, from);

Console.WriteLine($"Range: {from:yyyy-MM-dd} .. {to:yyyy-MM-dd}"
                  + (businessId is long bid ? $"  (business {bid})" : ""));
Console.WriteLine();

using var client = NexudusClient.WithPassword(username!, password!, totp, baseUrl);

try
{
    await ListInvoicesAsync(client, from, to, businessId);
    Console.WriteLine();
    await ListPaymentsAsync(client, from, to, businessId);
    return 0;
}
catch (NexudusApiException ex)
{
    Console.Error.WriteLine($"API error: {ex.Message}");
    if (ex.Errors is { Count: > 0 })
        foreach (var e in ex.Errors)
            Console.Error.WriteLine($"  {e.PropertyName}: {e.Message}");
    return 2;
}

// --- Invoices ---------------------------------------------------------------
static async Task ListInvoicesAsync(NexudusClient client, DateTimeOffset from, DateTimeOffset to, long? businessId)
{
    var invoices = new CoworkerInvoicesApi(client);

    // No dedicated filter class for CoworkerInvoice, so use the API's documented
    // `from_/to_<Entity>_<Field>` query convention via SearchParameters.Where(...).
    var filter = new SearchParameters { Size = 100 }
        .Where("from_CoworkerInvoice_CreatedOn", from)
        .Where("to_CoworkerInvoice_CreatedOn", to)
        .SortBy("CreatedOn", SortDirection.Ascending);
    if (businessId is long bid)
        filter.Where("CoworkerInvoice_Business", bid);

    Console.WriteLine("INVOICES");
    var count = 0;
    var totalsByCurrency = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

    // EnumerateCoworkerInvoices transparently follows pagination across all pages.
    await foreach (CoworkerInvoice inv in invoices.EnumerateCoworkerInvoices(filter))
    {
        count++;
        var currency = inv.CurrencyCode ?? "";
        totalsByCurrency[currency] = totalsByCurrency.GetValueOrDefault(currency) + inv.TotalAmount;

        var number = string.IsNullOrEmpty(inv.InvoiceNumber) ? $"(draft #{inv.Id})" : inv.InvoiceNumber;
        var status = inv.Paid ? "PAID" : (inv.Draft ? "DRAFT" : "DUE");
        Console.WriteLine($"  {inv.CreatedOn:yyyy-MM-dd}  {number,-16}  {inv.BillToName,-28}  "
                          + $"{inv.TotalAmount,12:N2} {currency}  [{status}]");
    }

    if (count == 0)
        Console.WriteLine("  (none)");
    else
        foreach (var (currency, total) in totalsByCurrency)
            Console.WriteLine($"  -> {count} invoice(s); total billed: {total:N2} {currency}");
}

// --- Payments ---------------------------------------------------------------
static async Task ListPaymentsAsync(NexudusClient client, DateTimeOffset from, DateTimeOffset to, long? businessId)
{
    var ledger = new CoworkerLedgerEntriesApi(client);

    // PaymentsBetween is the purpose-built helper: ledger entries by transaction
    // date. Payments are the credit entries, so we keep Credit > 0 below.
    var filter = new CoworkerLedgerEntryFilter { Size = 100 }
        .PaymentsBetween(from, to);
    if (businessId is long bid)
        filter.ForBusiness(bid);
    filter.SortBy("TransactionDate", SortDirection.Ascending);

    Console.WriteLine("PAYMENTS");
    var count = 0;
    var totalsByCurrency = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

    await foreach (CoworkerLedgerEntry entry in ledger.EnumerateCoworkerLedgerEntries(filter))
    {
        if (entry.Credit <= 0m)
            continue; // debits are charges/invoices, not payments

        count++;
        var currency = entry.BusinessCurrencyCode ?? "";
        totalsByCurrency[currency] = totalsByCurrency.GetValueOrDefault(currency) + entry.Credit;

        var who = entry.CoworkerFullName ?? entry.CoworkerInvoiceBillToName ?? "";
        var reference = entry.CoworkerInvoiceInvoiceNumber ?? entry.Description ?? entry.Code ?? "";
        Console.WriteLine($"  {entry.TransactionDate:yyyy-MM-dd}  {who,-28}  "
                          + $"{entry.Credit,12:N2} {currency}  {reference}");
    }

    if (count == 0)
        Console.WriteLine("  (none)");
    else
        foreach (var (currency, total) in totalsByCurrency)
            Console.WriteLine($"  -> {count} payment(s); total received: {total:N2} {currency}");
}

static DateTimeOffset? ParseDate(string? value) =>
    DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture,
        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var d)
        ? d
        : null;
