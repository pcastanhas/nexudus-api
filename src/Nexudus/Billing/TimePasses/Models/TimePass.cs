namespace Nexudus.Billing.TimePasses.Models;

/// <summary>
/// A pass that lets a customer check in to a space. A day pass (null <see cref="MinutesIncluded"/>)
/// is valid for a single day with unlimited check-ins; a time pass (with minutes) is valid across
/// days until the included minutes are used up.
/// </summary>
public sealed class TimePass : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? Name { get; set; }
    public string? InvoiceLineDisplayAs { get; set; }
    public decimal Price { get; set; }

    /// <summary>Minutes included; null means a single-day day pass.</summary>
    public int? MinutesIncluded { get; set; }

    public bool CountsTowardsPlanLimits { get; set; }
    public bool UseAsPayAsYouGoForMembers { get; set; }
    public bool UseAsPayAsYouGoForContacts { get; set; }
    public int? UsePriority { get; set; }

    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public int? TaxRateId { get; set; }
    public int? ReducedTaxRateId { get; set; }
    public int? ExemptTaxRateId { get; set; }
    public int? FinancialAccountId { get; set; }

    public List<long>? Businesses { get; set; }

    public string? KisiGroupId { get; set; }
    public string? DoorGuardGroupId { get; set; }
    public string? AccessControlGroupId { get; set; }
    public string? AccessControlGroupIds { get; set; }
    public bool AllowNetworkCheckIn { get; set; }

    public bool OnlyForContacts { get; set; }
    public bool OnlyForMembers { get; set; }
    public List<long>? Tariffs { get; set; }
    public bool Archived { get; set; }
}
