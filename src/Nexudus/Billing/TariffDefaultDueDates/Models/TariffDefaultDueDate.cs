namespace Nexudus.Billing.TariffDefaultDueDates.Models;

/// <summary>
/// Configures the default invoice due-date / auto-collection settings for one or more plans (tariffs).
/// </summary>
public sealed class TariffDefaultDueDate : NexudusEntity
{
    public int BusinessId { get; set; }
    public List<long>? Tariffs { get; set; }

    /// <summary>Day of the month on which to auto-collect.</summary>
    public int? AutoCollectOn { get; set; }

    /// <summary>Days after the invoice date on which to auto-collect.</summary>
    public int? AutoCollectAfter { get; set; }
}
