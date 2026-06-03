namespace Nexudus.Billing.Products.Models;

/// <summary>
/// An item that can be sold to customers — one-off or recurring — via contracts, bookings,
/// or direct purchase. Supports optional stock tracking and plan/audience restrictions.
/// </summary>
public sealed class Product : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? Name { get; set; }

    /// <summary>Category of the product.</summary>
    public ProductType SystemProductType { get; set; }

    public string? Description { get; set; }
    public string? InvoiceLineDisplayAs { get; set; }
    public string? Sku { get; set; }

    /// <summary>Comma-separated tags.</summary>
    public string? Tags { get; set; }
    public int DisplayOrder { get; set; }
    public decimal Price { get; set; }

    public bool Visible { get; set; }
    public bool VisibleInKiosk { get; set; }

    public bool AvailableToAi { get; set; }
    public string? NotesForAi { get; set; }
    public bool ShowPriceForAi { get; set; }
    public decimal? PriceForAi { get; set; }

    public bool SyncToSquare { get; set; }

    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public int? TaxRateId { get; set; }
    public int? ReducedTaxRateId { get; set; }
    public int? ExemptTaxRateId { get; set; }
    public int? FinancialAccountId { get; set; }

    /// <summary>Whether the product can be sold one-off, recurring, or both.</summary>
    public RecurrentProductOptions AvailableAs { get; set; }

    public bool OnlyForContacts { get; set; }
    public bool OnlyForMembers { get; set; }
    public List<long>? Tariffs { get; set; }

    public bool Archived { get; set; }
    public bool Starred { get; set; }

    public bool TrackStock { get; set; }
    public bool AllowNegativeStock { get; set; }
    public int? CurrentStock { get; set; }
    public int? StockAlertLevel { get; set; }

    public bool ApplyProRating { get; set; }

    /// <summary>Current image file name (read-only; upload via <see cref="NewImageUrl"/>).</summary>
    public string? ImageFileName { get; set; }
    /// <summary>URL of a new image to upload.</summary>
    public string? NewImageUrl { get; set; }
    /// <summary>Set true to remove the current image.</summary>
    public bool? ClearImageFile { get; set; }

    public bool InvoiceCoworker { get; set; }
    public bool SyncToNexKiosk { get; set; }
    public bool CreateDeliveryWhenPurchased { get; set; }
}

/// <summary>Category of a <see cref="Product"/> (<c>eProductType</c>).</summary>
public enum ProductType
{
    None = 0,
    DayPass = 1,
    CreditBundle = 2,
    Stationery = 3,
    BookingFeature = 4,
    BookingProducts = 5,
    Other = 99
}

/// <summary>Whether a product is sold one-off, recurring, or both (<c>eRecurrentProductOptions</c>).</summary>
public enum RecurrentProductOptions
{
    None = 0,
    RecurrentOrOneOff = 1,
    OnlyRecurrent = 2,
    OnlyOneOff = 3
}
