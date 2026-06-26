using System.Globalization;
using BcpRecordNexusActivity.Configuration;
using BcpRecordNexusActivity.Mapping;
using BcpRecordNexusActivity.Nexudus;

namespace BcpRecordNexusActivity.Journal;

/// <summary>
/// Turns the pulled Nexudus data into MRI JOURNAL rows.
/// <para>
/// First it resolves every invoice and payment against the maps; any unmatched entity or unmapped GL account
/// makes the whole run an error (no rows are produced). When everything resolves, it emits one balanced JE
/// (one <c>REF</c>) per entity per journal type. The <c>REF</c> sequence is continuous: all invoice JEs first
/// (entities ascending by <c>MRI_EntityID</c>), then all payment JEs, so e.g. invoices get N2605001/002/003
/// and payments continue at N2605004. <c>ITEM</c> restarts at 1 within each <c>REF</c>.
/// </para>
/// <para>
/// Per invoice detail line: a GL row (mapped account, <c>AMT = -SubTotal</c>) and an AR row
/// (<c>AMT = +SubTotal</c>). Per payment: a Cash row (<c>AMT = +amount</c>) and an AR row
/// (<c>AMT = -amount</c>). Positive = debit. Amounts are rounded to 2 dp (away from zero).
/// </para>
/// </summary>
public sealed class JournalBuilder
{
    private readonly MappingResolver _maps;

    public JournalBuilder(MappingResolver maps) => _maps = maps;

    private sealed record ResolvedInvoice(InvoiceRecord Invoice, EntityMapping Entity, List<(InvoiceLineItem Line, string MriGl)> Lines);
    private sealed record ResolvedPayment(PaymentRecord Payment, EntityMapping Entity);

    public BuildResult Build(NexudusDataSet data, string postingPeriod, DateTime runDate)
    {
        var errors = new List<BuildError>();
        var resolvedInvoices = new List<ResolvedInvoice>();
        var resolvedPayments = new List<ResolvedPayment>();

        // -- Phase 1: resolve & validate -------------------------------------
        foreach (var inv in data.Invoices)
        {
            var entity = _maps.ResolveEntity(inv.InvoiceNumber);
            if (entity is null)
            {
                errors.Add(new BuildError("Invoice", inv.InvoiceNumber, null,
                    "No entity mapping matches the invoice number."));
                continue;
            }

            var lines = new List<(InvoiceLineItem, string)>();
            foreach (var line in inv.Lines)
            {
                var gl = _maps.ResolveGlAccount(line.FinancialAccountCode);
                if (gl is null)
                {
                    errors.Add(new BuildError("Invoice", inv.InvoiceNumber, line.FinancialAccountCode,
                        string.IsNullOrWhiteSpace(line.FinancialAccountCode)
                            ? "Invoice line has no FinancialAccountCode."
                            : "No GL account mapping for FinancialAccountCode."));
                    continue;
                }
                lines.Add((line, gl));
            }

            resolvedInvoices.Add(new ResolvedInvoice(inv, entity, lines));
        }

        foreach (var pay in data.Payments)
        {
            if (string.IsNullOrWhiteSpace(pay.InvoiceNumber))
            {
                errors.Add(new BuildError("Payment", null, null,
                    "Payment is not linked to an invoice number, so its entity cannot be resolved."));
                continue;
            }

            var entity = _maps.ResolveEntity(pay.InvoiceNumber);
            if (entity is null)
            {
                errors.Add(new BuildError("Payment", pay.InvoiceNumber, null,
                    "No entity mapping matches the invoice number."));
                continue;
            }

            resolvedPayments.Add(new ResolvedPayment(pay, entity));
        }

        if (errors.Count > 0)
            return new BuildResult(Array.Empty<JournalRow>(), Array.Empty<JournalRow>(), errors);

        // -- Phase 2: emit rows ----------------------------------------------
        var sequence = 0;
        var invoiceRows = new List<JournalRow>();
        var paymentRows = new List<JournalRow>();

        // Invoice JEs first, one REF per entity, entities ascending by EntityID.
        foreach (var group in resolvedInvoices
                     .GroupBy(r => r.Entity.MriEntityId)
                     .OrderBy(g => EntityOrder(g.Key)))
        {
            var entity = group.First().Entity;
            var reference = JournalConstants.MakeRef(postingPeriod, ++sequence);
            var item = 0;

            foreach (var ri in group.OrderBy(r => r.Invoice.InvoiceNumber, StringComparer.Ordinal))
            {
                foreach (var (line, mriGl) in ri.Lines)
                {
                    var descr = Describe(ri.Invoice.InvoiceNumber, line.Description);
                    var amount = Round(line.SubTotal);
                    // GL/income leg (credit on a normal invoice) then AR leg (debit).
                    invoiceRows.Add(MakeRow(postingPeriod, reference, ++item, entity.MriEntityId, mriGl, -amount, descr, runDate));
                    invoiceRows.Add(MakeRow(postingPeriod, reference, ++item, entity.MriEntityId, entity.MriArAccount, amount, descr, runDate));
                }
            }
        }

        // Payment JEs next, sequence continues, one REF per entity.
        foreach (var group in resolvedPayments
                     .GroupBy(r => r.Entity.MriEntityId)
                     .OrderBy(g => EntityOrder(g.Key)))
        {
            var entity = group.First().Entity;
            var reference = JournalConstants.MakeRef(postingPeriod, ++sequence);
            var item = 0;

            foreach (var rp in group.OrderBy(r => r.Payment.InvoiceNumber, StringComparer.Ordinal))
            {
                var descr = Describe(rp.Payment.InvoiceNumber, rp.Payment.PayerName);
                var amount = Round(rp.Payment.Amount);
                // Cash leg (debit) then AR leg (credit).
                paymentRows.Add(MakeRow(postingPeriod, reference, ++item, entity.MriEntityId, entity.MriCashAccount, amount, descr, runDate));
                paymentRows.Add(MakeRow(postingPeriod, reference, ++item, entity.MriEntityId, entity.MriArAccount, -amount, descr, runDate));
            }
        }

        return new BuildResult(invoiceRows, paymentRows, Array.Empty<BuildError>());
    }

    private static JournalRow MakeRow(string period, string reference, int item, string entityId,
        string acctNum, decimal amt, string descr, DateTime runDate) =>
        new(
            Period: period,
            Ref: reference,
            Source: JournalConstants.Source,
            SiteId: JournalConstants.SiteId,
            Item: item,
            EntityId: entityId,
            AcctNum: acctNum,
            Department: JournalConstants.Department,
            Amt: amt,
            Descrpn: descr,
            EntrDate: runDate,
            Reversal: JournalConstants.Reversal,
            Status: JournalConstants.Status,
            OExchgRef: JournalConstants.OExchgRef,
            Basis: JournalConstants.Basis,
            LastDate: runDate,
            UserId: JournalConstants.UserId,
            QuickReversal: JournalConstants.QuickReversal);

    private static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

    /// <summary>"{invoiceNumber} {text}", newlines/tabs flattened to spaces, truncated to 75 chars.</summary>
    private static string Describe(string? invoiceNumber, string? text)
    {
        var combined = $"{invoiceNumber} {text}".Replace('\r', ' ').Replace('\n', ' ').Replace('\t', ' ').Trim();
        return combined.Length > JournalConstants.DescriptionMaxLength
            ? combined.Substring(0, JournalConstants.DescriptionMaxLength)
            : combined;
    }

    /// <summary>Numeric ordering of EntityID when possible, falling back to ordinal text.</summary>
    private static (long, string) EntityOrder(string entityId) =>
        long.TryParse(entityId, NumberStyles.Integer, CultureInfo.InvariantCulture, out var n)
            ? (n, entityId)
            : (long.MaxValue, entityId);
}
