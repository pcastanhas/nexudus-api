namespace Nexudus.Billing.CoworkerTimePasses.Models;

/// <summary>
/// A time pass assigned to a customer. Time passes can be assigned directly, granted by a product purchase
/// (<see cref="CoworkerProductUniqueId"/>), or included in a plan (<see cref="TariffTimePassUniqueId"/>).
/// <para>
/// Tracks usage (<see cref="Used"/>, <see cref="UsedDate"/>), remaining/total uses, and whether the customer
/// is currently checked in. Set <see cref="CreateMultiple"/> on create to issue several at once. Fields below
/// the <see cref="NexudusEntity"/> base mirror the "Get one CoworkerTimePass" response.
/// </para>
/// </summary>
public sealed class CoworkerTimePass : NexudusEntity
{
    public int CoworkerId { get; set; }
    public int BusinessId { get; set; }
    public int TimePassId { get; set; }
    public string? TimePassName { get; set; }
    public string? TimePassCurrencyCode { get; set; }
    public string? Notes { get; set; }
    public string? PurchaseOrder { get; set; }
    public bool Used { get; set; }
    public bool CheckedIn { get; set; }
    public DateTimeOffset? UsedDate { get; set; }
    public int? RemainingUses { get; set; }
    public int? TotalUses { get; set; }
    public bool Free { get; set; }
    public decimal? Price { get; set; }
    public int CreateMultiple { get; set; }
    public DateTimeOffset? ExpireDate { get; set; }
    public bool Invoiced { get; set; }
    public DateTimeOffset? InvoiceDate { get; set; }
    public bool IsFromTariff { get; set; }
    public bool IsPayAsYouGo { get; set; }
    public string? TariffTimePassUniqueId { get; set; }
    public string? CoworkerProductUniqueId { get; set; }
    public string? CoworkerContractUniqueId { get; set; }
}
