namespace Nexudus.Billing.Charges.Models;

/// <summary>
/// A generic debit item applied to a customer account.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one Charge" response.
/// Note that list/batch responses do not populate <see cref="DiscountAmount"/>,
/// <see cref="CreditAmount"/>, or <see cref="PurchaseOrder"/> — fetch the full record with
/// <c>GetOneCharge</c> before using those (and before any update).
/// </para>
/// </summary>
public sealed class Charge : NexudusEntity
{
    public int CoworkerId { get; set; }
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessCurrencyCode { get; set; }
    public string? ChargeNumber { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public string? InvoiceLineDisplayAs { get; set; }
    public bool RegularCharge { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string? DiscountCode { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PurchaseOrder { get; set; }
    public int? TaxRateId { get; set; }
    public int? FinancialAccountId { get; set; }
    public bool Invoiced { get; set; }
    public DateTimeOffset? InvoicedOn { get; set; }
    public DateTimeOffset? SaleDate { get; set; }
    public bool FromTeamMember { get; set; }
    public string? CoworkerExtraServiceName { get; set; }
    public string? CoworkerTimePassName { get; set; }
    public string? CoworkerProductName { get; set; }
    public string? TariffName { get; set; }
    public string? CoworkerProductUniqueId { get; set; }
    public string? BookingUniqueId { get; set; }
    public string? CoworkerContractUniqueId { get; set; }
    public string? CoworkerExtraServiceUniqueId { get; set; }
    public string? ExtraServiceUniqueId { get; set; }
    public string? CoworkerTimePassUniqueId { get; set; }
    public string? CoworkerChargeUniqueId { get; set; }
    public string? EventAttendeeUniqueId { get; set; }
    public DateTimeOffset? InvoiceFromDate { get; set; }
    public DateTimeOffset? InvoiceToDate { get; set; }
    public DateTimeOffset? RepeatFrom { get; set; }
    public DateTimeOffset? RepeatUntil { get; set; }
    public string? CoworkerDiscountCodeUniqueId { get; set; }
}
