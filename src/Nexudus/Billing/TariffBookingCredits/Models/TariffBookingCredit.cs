namespace Nexudus.Billing.TariffBookingCredits.Models;

/// <summary>
/// Credit attached to a plan (tariff). On contract renewal it is released as a
/// <c>CoworkerBookingCredit</c>. Can apply to bookings, events, and (as a universal credit)
/// products, passes, and other charges, optionally restricted by the eligibility lists.
/// Empty restriction lists mean "applies to all".
/// </summary>
public sealed class TariffBookingCredit : NexudusEntity
{
    public string? Name { get; set; }
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public string? TariffBusinessCurrencyCode { get; set; }

    public List<long>? ElegibleResourceTypes { get; set; }
    public List<long>? ElegibleProducts { get; set; }
    public List<long>? ElegibleTariffs { get; set; }

    public decimal Credit { get; set; }

    /// <summary>Whether the credit can pay for bookings (API spelling "Cane" preserved).</summary>
    public bool CaneBeUsedForBookings { get; set; }
    /// <summary>Whether the credit can pay for event sign-ups (API spelling "Cane" preserved).</summary>
    public bool CaneBeUsedForEvents { get; set; }
    public List<long>? EventCategories { get; set; }

    /// <summary>How often the credit renews.</summary>
    public TimeSpanWeekMonth ServiceRenewalTime { get; set; }

    public bool IsUniversalCredit { get; set; }
    public List<long>? ElegiblePasses { get; set; }
    public bool AppliesToCharges { get; set; }
}
