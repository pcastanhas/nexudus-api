namespace Nexudus.Billing.ResourceProducts.Models;

/// <summary>
/// Links a product to a resource, enabling products to be offered as add-ons when booking that resource
/// (e.g. catering, AV equipment, room setup).
/// <para>
/// If <see cref="Price"/> is null the underlying <see cref="ProductPrice"/> applies. Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one ResourceProduct" response.
/// </para>
/// </summary>
public sealed class ResourceProduct : NexudusEntity
{
    public int ResourceId { get; set; }
    public string? ResourceName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string? ProductCurrencyCode { get; set; }
    public bool InvoiceInMinutes { get; set; }
    public bool RequestQuantity { get; set; }
    public bool Visible { get; set; }
    public decimal? Price { get; set; }
}
