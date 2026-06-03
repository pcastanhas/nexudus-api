namespace Nexudus.Billing.TimePassTimeSlots.Models;

/// <summary>
/// Restricts when a time pass is valid: a day of the week plus a from/to time window.
/// </summary>
public sealed class TimePassTimeSlot : NexudusEntity
{
    public int TimePassId { get; set; }
    public DateTimeOffset? FromTime { get; set; }
    public DateTimeOffset? ToTime { get; set; }

    /// <summary>Day this slot applies to (matches <see cref="System.DayOfWeek"/>: Sunday = 0 … Saturday = 6).</summary>
    public DayOfWeek DayOfWeek { get; set; }
}
