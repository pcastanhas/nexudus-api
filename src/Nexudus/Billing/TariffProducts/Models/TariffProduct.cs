namespace Nexudus.Billing.TariffProducts.Models;

/// <summary>Links a product to a plan (tariff), including it as part of that plan's offering.</summary>
public sealed class TariffProduct : NexudusEntity
{
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string? ProductBusinessCurrencyCode { get; set; }
}
