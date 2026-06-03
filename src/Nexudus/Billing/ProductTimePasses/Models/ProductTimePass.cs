namespace Nexudus.Billing.ProductTimePasses.Models;

/// <summary>
/// Links a <c>TimePass</c> to a <c>Product</c>, granting check-in time on purchase.
/// For a day pass, <see cref="PassesIncluded"/> is a number of calendar days; for a time pass it
/// is a number of pass instances (each worth the time pass's included minutes).
/// </summary>
public sealed class ProductTimePass : NexudusEntity
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int TimePassId { get; set; }
    public string? TimePassName { get; set; }
    public int PassesIncluded { get; set; }

    public int? ExpireTimeInMonths { get; set; }
    public int? ExpireTimeInWeeks { get; set; }
    public int ExpirationType { get; set; }
    public int? ExpiresIn { get; set; }
}
