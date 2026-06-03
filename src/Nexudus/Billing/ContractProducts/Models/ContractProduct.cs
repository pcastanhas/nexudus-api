namespace Nexudus.Billing.ContractProducts.Models;

/// <summary>
/// A product added to a contract that is billed every time the contract is invoiced.
/// <para>
/// If <see cref="Price"/> is null the underlying <see cref="ProductPrice"/> is used; set it to override.
/// <see cref="RepeatFrom"/>/<see cref="RepeatUntil"/> bound the date range in which the product is invoiced
/// (leave both null to bill on every invoice). <see cref="ApplyProRating"/> only takes effect when the
/// location plan has prorating enabled. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one ContractProduct" response.
/// </para>
/// </summary>
public sealed class ContractProduct : NexudusEntity
{
    public int CoworkerContractId { get; set; }
    public int? CoworkerContractQuantity { get; set; }
    public string? CoworkerContractFloorPlanDeskIds { get; set; }
    public string? CoworkerContractFloorPlanDeskNames { get; set; }
    public string? CoworkerContractTariffName { get; set; }
    public int? CoworkerContractCoworkerId { get; set; }
    public string? CoworkerContractCoworkerFullName { get; set; }
    public string? CoworkerContractCoworkerBillingName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public bool? ProductApplyProRating { get; set; }
    public string? ProductCurrencyCode { get; set; }
    public string? Notes { get; set; }
    public int Quantity { get; set; }
    public decimal? Price { get; set; }
    public DateTimeOffset? RepeatFrom { get; set; }
    public DateTimeOffset? RepeatUntil { get; set; }
    public bool ApplyProRating { get; set; }
}
