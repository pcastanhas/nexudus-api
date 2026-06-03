namespace Nexudus.Billing.CoworkerExtraServiceUseHistories.Models;

/// <summary>
/// An audit/ledger record of how a customer's extra-service credits have been consumed, linking a
/// <c>CoworkerExtraService</c> allowance to the booking that spent it.
/// <para>
/// The data is logically a ledger view; although the API exposes write verbs, records are normally created
/// by the system as credits are consumed. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one CoworkerExtraServiceUseHistory" response.
/// </para>
/// </summary>
public sealed class CoworkerExtraServiceUseHistory : NexudusEntity
{
    public int CoworkerExtraServiceId { get; set; }
    public int? BookingId { get; set; }
    public DateTimeOffset? BookingFromTime { get; set; }
    public DateTimeOffset? BookingToTime { get; set; }
    public string? BookingResourceName { get; set; }
    public int? CreditUsed { get; set; }
}
