namespace Nexudus.Billing.TariffSignupProducts.Models;

/// <summary>
/// Links a product to a plan (tariff) as a one-time sign-up charge, automatically added to a
/// customer's first invoice when they join the plan.
/// </summary>
public sealed class TariffSignupProduct : NexudusEntity
{
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string? ProductCurrencyCode { get; set; }

    /// <summary>Sign-up price override (falls back to the product price when null).</summary>
    public decimal? Price { get; set; }
    public bool Refundable { get; set; }
    public bool InvoiceDuringOnlineCheckout { get; set; }
}
