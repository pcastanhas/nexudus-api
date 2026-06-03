namespace Nexudus.Billing.CoworkerInvoiceHistories.Models;

/// <summary>
/// A history entry for a customer invoice — an activity or event such as the invoice being sent, an
/// e-invoicing action, a payment receipt, or a payment error.
/// <para>
/// Set <see cref="IsProblem"/> to flag errors/issues, and <see cref="Notify"/> to trigger a notification.
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one CoworkerInvoiceHistory" response.
/// Several denormalised invoice fields are returned by the API as strings and are typed accordingly.
/// </para>
/// </summary>
public sealed class CoworkerInvoiceHistory : NexudusEntity
{
    public int CoworkerInvoiceId { get; set; }
    public int? CoworkerInvoiceCoworkerId { get; set; }
    public int? CoworkerInvoiceBusinessId { get; set; }
    public string? CoworkerInvoiceBusinessCurrencyCode { get; set; }
    public string? CoworkerInvoiceCoworkerFullName { get; set; }
    public string? CoworkerInvoiceTotalAmount { get; set; }
    public string? CoworkerInvoiceInvoiceNumber { get; set; }
    public string? CoworkerInvoiceBillToName { get; set; }
    public string? CoworkerInvoicePaid { get; set; }
    public DateTimeOffset? CoworkerInvoicePaidOn { get; set; }
    public bool? CoworkerInvoiceRefunded { get; set; }
    public DateTimeOffset? CoworkerInvoiceRefundedOn { get; set; }
    public DateTimeOffset? CoworkerInvoiceDueDate { get; set; }
    public string? CoworkerInvoiceDraft { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsProblem { get; set; }
    public bool Notify { get; set; }
}
