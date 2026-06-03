namespace Nexudus.Billing.CoworkerInvoiceLines.Models;

/// <summary>
/// An individual line item on a customer invoice, capturing description, quantity, amounts, tax, and an
/// optional link to the sale item that generated it (via one of the <c>*UniqueId</c> GUID properties).
/// <para>
/// The API supports search, get, and update for invoice lines but not create or delete, so this endpoint
/// derives from <see cref="ReadOnlyEndpoint{T}"/>. The denormalised <see cref="CoworkerInvoicePaid"/> and
/// <see cref="CoworkerInvoiceCreditNote"/> are returned as display strings here. Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one CoworkerInvoiceLine" response.
/// </para>
/// </summary>
public sealed class CoworkerInvoiceLine : NexudusEntity
{
    public int CoworkerInvoiceId { get; set; }
    public string? CoworkerInvoiceInvoiceNumber { get; set; }
    public string? CoworkerInvoicePaid { get; set; }
    public DateTimeOffset? CoworkerInvoicePaidOn { get; set; }
    public string? CoworkerInvoiceCreditNote { get; set; }
    public string? CoworkerInvoiceCurrencyCode { get; set; }
    public DateTimeOffset? CoworkerInvoiceDueDate { get; set; }
    public string? Description { get; set; }
    public string? DisplayAs { get; set; }
    public string? TaxCategoryName { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TaxRate { get; set; }
    public string? CoworkerContractUniqueId { get; set; }
    public string? ContractDepositUniqueId { get; set; }
    public string? BookingUniqueId { get; set; }
    public string? CoworkerExtraServiceUniqueId { get; set; }
    public string? ExtraServiceUniqueId { get; set; }
    public string? CoworkerTimePassUniqueId { get; set; }
    public string? CoworkerChargeUniqueId { get; set; }
    public string? CoworkerProductUniqueId { get; set; }
    public string? EventAttendeeUniqueId { get; set; }
    public string? GroupedLineUniqueId { get; set; }
    public decimal? RefundedAmount { get; set; }
    public bool Refunded { get; set; }
    public DateTimeOffset? RefundedOn { get; set; }
    public DateTimeOffset? SaleDate { get; set; }
    public string? DiscountCode { get; set; }
    public decimal? DiscountAmount { get; set; }
    public string? CoworkerExtraServiceName { get; set; }
    public string? CoworkerTimePassName { get; set; }
    public string? CoworkerProductName { get; set; }
    public string? EventAttendeeProductName { get; set; }
    public string? TariffName { get; set; }
    public string? FinancialAccountCode { get; set; }
    public string? FinancialAccountName { get; set; }
    public string? IssuedByUniqueId { get; set; }
    public string? CancelledCoworkerInvoiceLineUniqueId { get; set; }
    public DateTimeOffset? CreatedOnLocal { get; set; }
    public DateTimeOffset? RefundedOnLocal { get; set; }
    public DateTimeOffset? SaleDateLocal { get; set; }
    public int Position { get; set; }
    public bool IsHidden { get; set; }
    public bool IsUniversalCredit { get; set; }
    public string? PurchaseOrder { get; set; }
    public decimal CreditAmount { get; set; }
    public bool IsProratedContract { get; set; }
    public string? CoworkerDiscountCodeUniqueId { get; set; }
}
