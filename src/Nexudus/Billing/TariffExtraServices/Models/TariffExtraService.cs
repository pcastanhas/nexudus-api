namespace Nexudus.Billing.TariffExtraServices.Models;

/// <summary>
/// Links an extra service (booking rate) to a plan (tariff), defining an included allowance of
/// booking time or printing credits. <see cref="UsesIncluded"/> sets how much is included.
/// </summary>
public sealed class TariffExtraService : NexudusEntity
{
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public int ExtraServiceId { get; set; }
    public string? ExtraServiceName { get; set; }

    // The API projects these related ExtraService attributes as strings.
    public string? ExtraServiceChargePeriod { get; set; }
    public string? ExtraServiceIsBookingCredit { get; set; }
    public string? ExtraServiceIsPrintingCredit { get; set; }

    public int UsesIncluded { get; set; }

    /// <summary>How often the included allowance renews.</summary>
    public TimeSpanWeekMonth ServiceRenewalTime { get; set; }
}
