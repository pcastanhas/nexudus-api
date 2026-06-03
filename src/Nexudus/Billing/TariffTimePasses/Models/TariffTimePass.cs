namespace Nexudus.Billing.TariffTimePasses.Models;

/// <summary>Links a time pass to a plan (tariff), defining an included pass allowance per renewal period.</summary>
public sealed class TariffTimePass : NexudusEntity
{
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public int TimePassId { get; set; }
    public string? TimePassName { get; set; }
    public int PassesIncluded { get; set; }

    /// <summary>How often the included pass allowance resets.</summary>
    public TimeSpanWeekMonth PassRenewalTime { get; set; }
}
