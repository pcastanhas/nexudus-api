namespace Nexudus.Billing.ExtraServiceTimeSlots.Models;

/// <summary>
/// Restricts when an <c>ExtraService</c> price applies: a day of the week plus a from/to time
/// window. Only the time-of-day part of <see cref="FromTime"/>/<see cref="ToTime"/> is meaningful
/// (the date component is always 1976-01-01). With no slots defined, the price applies at all times.
/// </summary>
public sealed class ExtraServiceTimeSlot : NexudusEntity
{
    public int ExtraServiceId { get; set; }
    public DateTimeOffset? FromTime { get; set; }
    public DateTimeOffset? ToTime { get; set; }

    /// <summary>Day this slot applies to (matches <see cref="System.DayOfWeek"/>: Sunday = 0 … Saturday = 6).</summary>
    public DayOfWeek DayOfWeek { get; set; }

    /// <summary>When false, the slot is an explicit block rather than an availability window.</summary>
    public bool? Available { get; set; }
}
