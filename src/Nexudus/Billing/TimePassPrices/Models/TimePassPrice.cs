namespace Nexudus.Billing.TimePassPrices.Models;

/// <summary>A pricing tier for a time pass — a price that can vary by plan (tariff) or segment.</summary>
public sealed class TimePassPrice : NexudusEntity
{
    public int TimePassId { get; set; }
    public string? TimePassName { get; set; }
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public decimal Price { get; set; }
}
