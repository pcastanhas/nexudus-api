namespace Nexudus.Billing.BusinessCharges.Models;

/// <summary>
/// A charge issued by Nexudus to a location for platform services or subscription fees (managed by Nexudus
/// staff). Can be one-off or recurring, and moves through a two-party approval workflow before invoicing.
/// <para>
/// Set <see cref="Recurrent"/> with <see cref="RepeatFrom"/>/<see cref="RepeatUntil"/> for recurrence; both
/// <see cref="ApprovedByBusiness"/> and <see cref="ApprovedBySender"/> must be true before the charge can be
/// invoiced. Fields below the <see cref="NexudusEntity"/> base mirror the "Get one BusinessCharge" response.
/// </para>
/// </summary>
public sealed class BusinessCharge : NexudusEntity
{
    public int BusinessId { get; set; }
    public int? ApplicationId { get; set; }
    public string? Description { get; set; }
    public string? CallBackUrl { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public decimal PercentageDiscount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public bool Invoiced { get; set; }
    public DateTimeOffset? InvoicedOn { get; set; }
    public bool ApprovedByBusiness { get; set; }
    public bool ApprovedBySender { get; set; }
    public bool Recurrent { get; set; }
    public DateTimeOffset? RepeatFrom { get; set; }
    public DateTimeOffset? RepeatUntil { get; set; }
}
