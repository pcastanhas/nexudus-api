namespace Nexudus.Billing.DiscountCodes.Models;

/// <summary>
/// A discount (percentage or fixed amount) that can be applied to plans, bookings, products,
/// and/or events, optionally restricted by date, usage caps, and audience.
/// </summary>
public sealed class DiscountCode : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessCurrencyCode { get; set; }

    /// <summary>The alphanumeric code customers enter to apply the discount.</summary>
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }

    public DateTimeOffset? PublishFrom { get; set; }
    public DateTimeOffset? PublishTo { get; set; }

    /// <summary>Percentage off (e.g. 10 = 10%). Mutually exclusive with <see cref="DiscountAmount"/>.</summary>
    public decimal? DiscountPercentage { get; set; }

    /// <summary>Fixed amount off. Mutually exclusive with <see cref="DiscountPercentage"/>.</summary>
    public decimal? DiscountAmount { get; set; }

    public bool ReferralDiscount { get; set; }

    public bool DiscountPricePlans { get; set; }
    public List<long>? Tariffs { get; set; }
    public bool DiscountBookings { get; set; }
    public List<long>? ResourceTypes { get; set; }
    public bool DiscountProducts { get; set; }
    public List<long>? Products { get; set; }
    public bool DiscountEvents { get; set; }
    public List<long>? EventCategories { get; set; }

    public int? MaxUsesPerUser { get; set; }
    public int? MaxUses { get; set; }
    public bool OnlyForContacts { get; set; }
    public bool OnlyForMembers { get; set; }

    public DateTimeOffset? ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }

    /// <summary>Unit of the expiration period (Day / Week / Month / Year), used with <see cref="ExpiresIn"/>.</summary>
    public int ExpirationType { get; set; }
    public int? ExpiresIn { get; set; }
}
