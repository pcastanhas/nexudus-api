namespace Nexudus.Billing.ProductExtraServices.Models;

/// <summary>
/// Links an <c>ExtraService</c> to a <c>Product</c>, granting an allowance of booking time or
/// printing credits on purchase. The unit of <see cref="UsesIncluded"/> depends on the linked
/// extra service's charge period (and whether it is a printing credit).
/// </summary>
public sealed class ProductExtraService : NexudusEntity
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int ExtraServiceId { get; set; }
    public string? ExtraServiceName { get; set; }

    /// <summary>Charge period of the linked extra service (projected as a string by the API).</summary>
    public string? ExtraServiceChargePeriod { get; set; }
    public bool ExtraServiceIsBookingCredit { get; set; }
    public bool ExtraServiceIsPrintingCredit { get; set; }

    public int UsesIncluded { get; set; }
    public int? ExpireTimeInMonths { get; set; }
    public int? ExpireTimeInWeeks { get; set; }
    public int ExpirationType { get; set; }
    public int? ExpiresIn { get; set; }
}
