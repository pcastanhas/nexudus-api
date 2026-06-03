namespace Nexudus.Billing.CoworkerBookingCreditUseHistories.Models;

/// <summary>
/// Records each time a <c>CoworkerBookingCredit</c> was consumed, capturing the amount deducted and linking
/// back to the booking, event attendance, or invoice line that triggered the use.
/// <para>
/// The denormalised fields (<see cref="BookingResourceName"/>, <see cref="EventAttendeeCalendarEventName"/>,
/// etc.) describe what the credit was spent on. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one CoworkerBookingCreditUseHistory" response. This entity has no delete endpoint.
/// </para>
/// </summary>
public sealed class CoworkerBookingCreditUseHistory : NexudusEntity
{
    public string? Description { get; set; }
    public int CoworkerBookingCreditId { get; set; }
    public int? BookingId { get; set; }
    public DateTimeOffset? BookingFromTime { get; set; }
    public DateTimeOffset? BookingToTime { get; set; }
    public string? BookingResourceName { get; set; }
    public int? CoworkerInvoiceLineId { get; set; }
    public int? CoworkerInvoiceLineCoworkerInvoiceId { get; set; }
    public string? CoworkerInvoiceLineCoworkerInvoiceInvoiceNumber { get; set; }
    public int? EventAttendeeId { get; set; }
    public string? EventAttendeeCalendarEventName { get; set; }
    public string? EventAttendeeEventProductName { get; set; }
    public string? EventAttendeeFullName { get; set; }
    public string? EventAttendeeEmail { get; set; }
    public decimal CreditUsed { get; set; }
}
