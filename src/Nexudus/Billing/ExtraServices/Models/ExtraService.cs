namespace Nexudus.Billing.ExtraServices.Models;

/// <summary>
/// A resource-type pricing rule (a "booking rate") or, when <see cref="IsPrintingCredit"/> is true,
/// a printing allowance. Defines how resource types are billed, with optional time, length,
/// audience, date-range, and dynamic-pricing restrictions.
/// </summary>
public sealed class ExtraService : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? InvoiceLineDisplayAs { get; set; }
    public bool Visible { get; set; }
    public int DisplayOrder { get; set; }

    public List<long>? ResourceTypes { get; set; }

    public decimal Price { get; set; }
    public decimal? CreditPrice { get; set; }
    public ChargePeriod ChargePeriod { get; set; }
    public decimal? MaximumPrice { get; set; }
    public bool IsDefaultPrice { get; set; }
    public bool UsePerNightPricing { get; set; }

    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public int? TaxRateId { get; set; }
    public int? ReducedTaxRateId { get; set; }
    public int? ExemptTaxRateId { get; set; }
    public int? FinancialAccountId { get; set; }

    /// <summary>Start-time restriction, in minutes from midnight.</summary>
    public int? FromTime { get; set; }
    /// <summary>End-time restriction, in minutes from midnight.</summary>
    public int? ToTime { get; set; }
    /// <summary>Minimum booking length, in minutes.</summary>
    public int? MinLength { get; set; }
    /// <summary>Maximum booking length, in minutes.</summary>
    public int? MaxLength { get; set; }
    public bool OnlyWithinAvailableTimes { get; set; }
    public int? FixedCostLength { get; set; }
    public decimal? FixedCostPrice { get; set; }

    public List<long>? Tariffs { get; set; }
    public bool OnlyForContacts { get; set; }
    public bool OnlyForMembers { get; set; }
    public bool IsBookingCredit { get; set; }
    public bool IsPrintingCredit { get; set; }
    public bool ApplyChargeToVisitors { get; set; }

    public decimal? PriceFactorLowDemand { get; set; }
    public decimal? PriceFactorAverageDemand { get; set; }
    public decimal? PriceFactorHighDemand { get; set; }
    public decimal? PriceFactorLastMinute { get; set; }
    public int? LastMinutePeriodMinutes { get; set; }
    public LastMinuteDiscountType LastMinuteAdjustmentType { get; set; }

    public DateTimeOffset? ApplyFrom { get; set; }
    public DateTimeOffset? ApplyTo { get; set; }

    /// <summary>Comma-separated names of associated resource types (read-only).</summary>
    public string? ResourceTypeNames { get; set; }
    public List<long>? Teams { get; set; }
}
