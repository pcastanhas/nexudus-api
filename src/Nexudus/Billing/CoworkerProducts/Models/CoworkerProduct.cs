namespace Nexudus.Billing.CoworkerProducts.Models;

/// <summary>
/// A record of a product sold to a customer as a one-off or recurring charge.
/// <para>
/// <see cref="RepeatCycle"/> may only be <c>PricePlan</c> if the customer has a main contract; prefer
/// <c>ContractProduct</c> over CoworkerProducts repeating on the plan. <see cref="ActivateNow"/> releases
/// benefits before invoicing/payment. The <c>*UniqueId</c> properties link the sale to its originating
/// record (only one is populated). Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one CoworkerProduct" response.
/// </para>
/// </summary>
public sealed class CoworkerProduct : NexudusEntity
{
    public int CoworkerId { get; set; }
    public string? CoworkerCoworkerType { get; set; }
    public string? CoworkerFullName { get; set; }
    public string? CoworkerCompanyName { get; set; }
    public string? CoworkerBillingName { get; set; }
    public string? CoworkerEmail { get; set; }
    public int BusinessId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public bool? ProductApplyProRating { get; set; }
    public string? ProductCurrencyCode { get; set; }
    public string? Notes { get; set; }
    public string? PurchaseOrder { get; set; }
    public string? OrderNumber { get; set; }
    public bool Activated { get; set; }
    public bool ActivateNow { get; set; }
    public bool InvoiceThisCoworker { get; set; }
    public decimal? Price { get; set; }
    public int Quantity { get; set; }
    public bool RegularCharge { get; set; }
    public RecurrentChargePattern RepeatCycle { get; set; }
    public int? RepeatUnit { get; set; }
    public DateTimeOffset? InvoiceOn { get; set; }
    public DateTimeOffset? RepeatFrom { get; set; }
    public DateTimeOffset? RepeatUntil { get; set; }
    public DateTimeOffset? SaleDate { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public bool Invoiced { get; set; }
    public DateTimeOffset? InvoicedOn { get; set; }
    public bool FromTariff { get; set; }
    public string? BookingUniqueId { get; set; }
    public bool MrmReminded { get; set; }
    public bool ApplyProRating { get; set; }
    public string? CoworkerContractUniqueId { get; set; }
    public string? ContractDepositUniqueId { get; set; }
    public string? ContractProductUniqueId { get; set; }
    public string? CoworkerDeliveryUniqueId { get; set; }
    public string? ProposalUniqueId { get; set; }
    public int? CoworkerInvoiceId { get; set; }
    public string? CoworkerInvoiceNumber { get; set; }
    public bool CoworkerInvoicePaid { get; set; }
    public string? TeamsAtTheTimeOfPurchase { get; set; }
    public decimal CreditAmount { get; set; }
    public decimal DiscountAmount { get; set; }
}
