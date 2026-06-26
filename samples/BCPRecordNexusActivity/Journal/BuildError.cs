namespace BcpRecordNexusActivity.Journal;

/// <summary>A mapping/validation failure for one invoice or payment. Collected, reported, and emailed.</summary>
public sealed record BuildError(
    string Kind,                     // "Invoice" or "Payment"
    string? Reference,               // invoice number, if known
    string? FinancialAccountCode,    // for unmapped-GL errors
    string Reason);

/// <summary>Outcome of building the journals: either rows (success) or errors (nothing posted).</summary>
public sealed class BuildResult
{
    public IReadOnlyList<JournalRow> InvoiceRows { get; }
    public IReadOnlyList<JournalRow> PaymentRows { get; }
    public IReadOnlyList<BuildError> Errors { get; }

    public BuildResult(IReadOnlyList<JournalRow> invoiceRows, IReadOnlyList<JournalRow> paymentRows, IReadOnlyList<BuildError> errors)
    {
        InvoiceRows = invoiceRows;
        PaymentRows = paymentRows;
        Errors = errors;
    }

    public bool HasErrors => Errors.Count > 0;
    public IEnumerable<JournalRow> AllRows => InvoiceRows.Concat(PaymentRows);
}
