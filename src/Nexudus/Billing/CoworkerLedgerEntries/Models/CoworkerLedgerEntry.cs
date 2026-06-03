namespace Nexudus.Billing.CoworkerLedgerEntries.Models;

/// <summary>
/// An individual financial transaction line in a customer's ledger, recording debits, credits, and the
/// running balance between a location and a customer.
/// <para>
/// Payments appear as <see cref="Credit"/> entries; a positive <see cref="Balance"/> means the customer owes
/// money, a negative balance means they have credit on account. <see cref="PaymentGatewayName"/> identifies
/// the provider that processed the transaction. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one CoworkerLedgerEntry" response; the denormalised <see cref="CoworkerInvoiceTotalAmount"/> is
/// returned by the API as a string.
/// </para>
/// </summary>
public sealed class CoworkerLedgerEntry : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessCurrencyCode { get; set; }
    public int CoworkerId { get; set; }
    public string? CoworkerFullName { get; set; }
    public int? CoworkerInvoiceId { get; set; }
    public string? CoworkerInvoiceInvoiceNumber { get; set; }
    public string? CoworkerInvoiceTotalAmount { get; set; }
    public string? CoworkerInvoiceBillToName { get; set; }
    public bool? CoworkerInvoicePaid { get; set; }
    public DateTimeOffset? CoworkerInvoicePaidOn { get; set; }
    public bool? CoworkerInvoiceRefunded { get; set; }
    public DateTimeOffset? CoworkerInvoiceRefundedOn { get; set; }
    public DateTimeOffset? CoworkerInvoiceDueDate { get; set; }
    public bool? CoworkerInvoiceDraft { get; set; }
    public bool? CoworkerInvoiceWaitingForInvoiceNumber { get; set; }
    public string? Description { get; set; }
    public string? Code { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public PaymentProvider PaymentGatewayName { get; set; }
    public string? PaymentMethodNumber { get; set; }
    public DateTimeOffset? TransactionDate { get; set; }
    public decimal Balance { get; set; }
    public bool Billed { get; set; }
    public DateTimeOffset? TransactionDateLocal { get; set; }
    public string? ConnectedTransactionGuid { get; set; }
}
