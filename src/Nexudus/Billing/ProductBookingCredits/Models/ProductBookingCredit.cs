namespace Nexudus.Billing.ProductBookingCredits.Models;

/// <summary>
/// Credit attached to a <c>Product</c>. When the product is purchased it is released as a
/// <c>CoworkerBookingCredit</c>. Can apply to bookings, events, and (as a universal credit)
/// products, passes, and other charges; empty eligibility lists mean "applies to all".
/// </summary>
public sealed class ProductBookingCredit : NexudusEntity
{
    public string? Name { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductBusinessCurrencyCode { get; set; }

    public List<long>? ElegibleResourceTypes { get; set; }
    public List<long>? ElegibleProducts { get; set; }
    public List<long>? ElegibleTariffs { get; set; }

    public decimal Credit { get; set; }
    public int? ExpireTimeInMonths { get; set; }
    public int? ExpireTimeInWeeks { get; set; }

    /// <summary>Whether the credit can pay for bookings (API spelling "Cane" preserved).</summary>
    public bool CaneBeUsedForBookings { get; set; }
    /// <summary>Whether the credit can pay for event sign-ups (API spelling "Cane" preserved).</summary>
    public bool CaneBeUsedForEvents { get; set; }
    public List<long>? EventCategories { get; set; }

    public int ExpirationType { get; set; }
    public int? ExpiresIn { get; set; }

    public bool IsUniversalCredit { get; set; }
    public List<long>? ElegiblePasses { get; set; }
    public bool AppliesToCharges { get; set; }
}
