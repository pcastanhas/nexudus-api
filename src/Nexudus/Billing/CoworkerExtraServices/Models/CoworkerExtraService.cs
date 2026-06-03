namespace Nexudus.Billing.CoworkerExtraServices.Models;

/// <summary>
/// A charge or credit assigned to a customer. Covers booking charges (linked via <see cref="BookingId"/>),
/// time credit (booking allowances measured in the linked service's <see cref="ChargePeriod"/> units), and
/// printing credit (when the linked extra service has <c>IsPrintingCredit = true</c>).
/// <para>
/// <see cref="TotalUses"/>/<see cref="RemainingUses"/> track the allowance. When <see cref="IsFromTariff"/>
/// is true the record was provisioned by a contract (<see cref="CoworkerContractUniqueId"/>). Fields below
/// the <see cref="NexudusEntity"/> base mirror the "Get one CoworkerExtraService" response.
/// </para>
/// </summary>
public sealed class CoworkerExtraService : NexudusEntity
{
    public int CoworkerId { get; set; }
    public int BusinessId { get; set; }
    public int ExtraServiceId { get; set; }
    public string? ExtraServiceName { get; set; }
    public string? ExtraServiceCurrencyCode { get; set; }
    public bool ExtraServiceIsPrintingCredit { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int RemainingUses { get; set; }
    public int TotalUses { get; set; }
    public bool Free { get; set; }
    public decimal? Price { get; set; }
    public decimal? LastMinutePriceAdjustment { get; set; }
    public decimal? DynamicPriceAdjustment { get; set; }
    public decimal? PriceFactorLastMinute { get; set; }
    public decimal? PriceFactorDemand { get; set; }
    public DateTimeOffset? ValidFrom { get; set; }
    public DateTimeOffset? ExpireDate { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public string? PurchaseOrder { get; set; }
    public ChargePeriod ChargePeriod { get; set; }
    public bool Invoiced { get; set; }
    public DateTimeOffset? InvoiceDate { get; set; }
    public bool IsFromTariff { get; set; }
    public string? TariffTimePassUniqueId { get; set; }
    public string? CoworkerProductUniqueId { get; set; }
    public string? BookingUniqueId { get; set; }
    public bool AutomaticallyAdded { get; set; }
    public bool InvoiceThisCoworker { get; set; }
    public string? DiscountCode { get; set; }
    public string? CoworkerDiscountUniqueId { get; set; }
    public decimal? DiscountAmount { get; set; }
    public int? BookingId { get; set; }
    public DateTimeOffset? BookingFromTime { get; set; }
    public DateTimeOffset? BookingToTime { get; set; }
    public string? BookingResourceName { get; set; }
    public string? CoworkerContractUniqueId { get; set; }
}
