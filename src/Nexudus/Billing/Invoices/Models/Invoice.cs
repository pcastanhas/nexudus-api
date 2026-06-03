namespace Nexudus.Billing.Invoices.Models;

/// <summary>
/// A bill issued by Nexudus to the operator (the business running the space). Captures a snapshot of the
/// billing details at issuance time; once issued, those captured values are immutable.
/// <para>
/// The API supports search, get, and update for invoices but not create or delete, so this endpoint derives
/// from <see cref="ReadOnlyEndpoint{T}"/>. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one Invoice" response.
/// </para>
/// </summary>
public sealed class Invoice : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? BillToName { get; set; }
    public string? BillToAddress { get; set; }
    public string? BillToCity { get; set; }
    public string? BillToTaxIDNumber { get; set; }
    public string? BillToPostCode { get; set; }
    public string? BillToPhone { get; set; }
    public string? BillToFax { get; set; }
    public int BillToCountryId { get; set; }
    public string? BillToCountryName { get; set; }
    public string? Description { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? InvoiceFromDate { get; set; }
    public DateTimeOffset? InvoiceToDate { get; set; }
    public decimal SubscriptionAmount { get; set; }
    public decimal ResellerAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal TaxAmount { get; set; }
    public bool Paid { get; set; }
    public DateTimeOffset? PaidOn { get; set; }
    public string? CustomData { get; set; }
    public int PaymentAttemptsCount { get; set; }
}
