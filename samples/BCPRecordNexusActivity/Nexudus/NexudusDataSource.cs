using Nexudus;
using Nexudus.Billing;
using Nexudus.Billing.CoworkerInvoices.API;
using Nexudus.Billing.CoworkerInvoiceLines.API;
using Nexudus.Billing.CoworkerLedgerEntries.API;

namespace BcpRecordNexusActivity.Nexudus;

/// <summary>Loads the invoices and payments for a run. Abstracted so the pipeline can be tested offline.</summary>
public interface INexudusDataSource
{
    Task<NexudusDataSet> LoadAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default);
}

/// <summary>
/// Live implementation backed by the Nexudus REST client.
/// <para>
/// Invoices = <c>CoworkerInvoice</c> by <c>CreatedOn</c> in range, excluding drafts and voids (credit notes
/// are kept), each with its <c>CoworkerInvoiceLine</c>s. Payments = <c>CoworkerLedgerEntry</c> by
/// <c>TransactionDate</c> in range — the same selection the InvoicesAndPayments sample's
/// <c>ListPaymentsAsync</c> uses, extended to include refunds. A payment is a credit (amount = +Credit); a
/// refund is a debit that went through a payment gateway (amount = -Debit). Pure invoice-charge debits
/// (no gateway) are skipped so charges don't leak into the receipt journal.
/// </para>
/// </summary>
public sealed class NexudusDataSource : INexudusDataSource
{
    private readonly NexudusClient _client;

    public NexudusDataSource(NexudusClient client) => _client = client;

    public async Task<NexudusDataSet> LoadAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken = default)
    {
        var invoices = await LoadInvoicesAsync(from, to, cancellationToken).ConfigureAwait(false);
        var payments = await LoadPaymentsAsync(from, to, cancellationToken).ConfigureAwait(false);
        return new NexudusDataSet(invoices, payments);
    }

    private async Task<List<InvoiceRecord>> LoadInvoicesAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct)
    {
        var invoicesApi = new CoworkerInvoicesApi(_client);
        var linesApi = new CoworkerInvoiceLinesApi(_client);
        var result = new List<InvoiceRecord>();

        var filter = new SearchParameters { Size = 100 }
            .Where("from_CoworkerInvoice_CreatedOn", from)
            .Where("to_CoworkerInvoice_CreatedOn", to)
            .SortBy("CreatedOn", SortDirection.Ascending);

        await foreach (var inv in invoicesApi.EnumerateCoworkerInvoices(filter, ct).ConfigureAwait(false))
        {
            if (inv.Draft || inv.Void)
                continue; // credit notes (CreditNote == true) are intentionally kept

            var lineFilter = new SearchParameters { Size = 100 }
                .Where("CoworkerInvoiceLine_CoworkerInvoice", inv.Id)
                .SortBy("Position", SortDirection.Ascending);

            var lines = new List<InvoiceLineItem>();
            await foreach (var line in linesApi.EnumerateCoworkerInvoiceLines(lineFilter, ct).ConfigureAwait(false))
                lines.Add(new InvoiceLineItem(line.FinancialAccountCode, line.SubTotal, line.Description));

            result.Add(new InvoiceRecord(inv.InvoiceNumber, lines));
        }

        return result;
    }

    private async Task<List<PaymentRecord>> LoadPaymentsAsync(DateTimeOffset from, DateTimeOffset to, CancellationToken ct)
    {
        var ledger = new CoworkerLedgerEntriesApi(_client);
        var result = new List<PaymentRecord>();

        var filter = new CoworkerLedgerEntryFilter { Size = 100 }
            .PaymentsBetween(from, to);
        filter.SortBy("TransactionDate", SortDirection.Ascending);

        await foreach (var entry in ledger.EnumerateCoworkerLedgerEntries(filter, ct).ConfigureAwait(false))
        {
            var amount = entry.Credit - entry.Debit; // payment positive, refund negative
            var isCashMovement =
                entry.Credit > 0m ||
                (entry.Debit > 0m && entry.PaymentGatewayName != PaymentProvider.None);

            if (!isCashMovement || amount == 0m)
                continue;

            var payer = entry.CoworkerInvoiceBillToName ?? entry.CoworkerFullName;
            result.Add(new PaymentRecord(entry.CoworkerInvoiceInvoiceNumber, payer, amount));
        }

        return result;
    }
}
