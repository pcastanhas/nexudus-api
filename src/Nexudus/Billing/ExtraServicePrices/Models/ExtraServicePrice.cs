namespace Nexudus.Billing.ExtraServicePrices.Models;

/// <summary>
/// An overriding price for an <c>ExtraService</c> (resource price) applied to members on a
/// specific plan (tariff), used instead of the default extra-service price.
/// </summary>
public sealed class ExtraServicePrice : NexudusEntity
{
    public int ExtraServiceId { get; set; }
    public string? ExtraServiceName { get; set; }
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public decimal Price { get; set; }
    public decimal? MaximumPrice { get; set; }
}
