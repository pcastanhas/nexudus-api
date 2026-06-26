namespace BcpRecordNexusActivity.Nexudus;

/// <summary>A single Nexudus invoice detail line, reduced to what the journal needs.</summary>
public sealed record InvoiceLineItem(string? FinancialAccountCode, decimal SubTotal, string? Description);

/// <summary>A Nexudus customer invoice plus its detail lines.</summary>
public sealed record InvoiceRecord(string? InvoiceNumber, IReadOnlyList<InvoiceLineItem> Lines);

/// <summary>A cash movement from the customer ledger (payment positive, refund negative).</summary>
public sealed record PaymentRecord(string? InvoiceNumber, string? PayerName, decimal Amount);

/// <summary>Everything pulled from Nexudus for one run.</summary>
public sealed record NexudusDataSet(
    IReadOnlyList<InvoiceRecord> Invoices,
    IReadOnlyList<PaymentRecord> Payments);
