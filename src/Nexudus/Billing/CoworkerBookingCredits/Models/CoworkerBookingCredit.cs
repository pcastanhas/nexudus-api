namespace Nexudus.Billing.CoworkerBookingCredits.Models;

/// <summary>
/// An amount of monetary credit assigned to a customer, usually created automatically when a contract on a
/// plan with a <c>TariffBookingCredit</c> renews.
/// <para>
/// Configure usage with <see cref="CaneBeUsedForBookings"/> (restricted by <see cref="ElegibleResourceTypes"/>),
/// <see cref="CaneBeUsedForEvents"/> (restricted by <see cref="EventCategories"/>), and
/// <see cref="IsUniversalCredit"/> (restricted by <see cref="ElegibleProducts"/>, <see cref="ElegiblePasses"/>,
/// and <see cref="AppliesToCharges"/>); empty restriction lists mean "applies to all". Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one CoworkerBookingCredit" response.
/// </para>
/// </summary>
public sealed class CoworkerBookingCredit : NexudusEntity
{
    public int CoworkerId { get; set; }
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessCurrencyCode { get; set; }
    public string? Description { get; set; }
    public int? TariffBookingCreditId { get; set; }
    public string? TariffBookingCreditName { get; set; }
    public List<long>? ElegibleResourceTypes { get; set; }
    public List<long>? ElegibleProducts { get; set; }
    public List<long>? ElegibleTariffs { get; set; }
    public decimal RemainingCredit { get; set; }
    public decimal TotalCredit { get; set; }
    public DateTimeOffset? ValidFrom { get; set; }
    public DateTimeOffset? ExpireDate { get; set; }
    public bool CaneBeUsedForBookings { get; set; }
    public bool CaneBeUsedForEvents { get; set; }
    public List<long>? EventCategories { get; set; }
    public bool IsUniversalCredit { get; set; }
    public string? CoworkerProductUniqueId { get; set; }
    public bool UseCreditPrice { get; set; }
    public string? CoworkerContractUniqueId { get; set; }
    public List<long>? ElegiblePasses { get; set; }
    public bool AppliesToCharges { get; set; }
}
