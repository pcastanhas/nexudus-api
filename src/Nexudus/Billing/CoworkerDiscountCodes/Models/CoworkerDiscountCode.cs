namespace Nexudus.Billing.CoworkerDiscountCodes.Models;

/// <summary>
/// Assigns a <c>DiscountCode</c> to a specific customer, tracking per-customer redemption history and
/// individual validity windows.
/// <para>
/// <see cref="ValidFrom"/>/<see cref="ExpiresOn"/> are customer-specific and distinct from the discount
/// code's own validity; the system enforces whichever constraint is more restrictive. Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one CoworkerDiscountCode" response.
/// </para>
/// </summary>
public sealed class CoworkerDiscountCode : NexudusEntity
{
    public int CoworkerId { get; set; }
    public string? CoworkerCoworkerType { get; set; }
    public string? CoworkerFullName { get; set; }
    public string? CoworkerBillingName { get; set; }
    public string? CoworkerCompanyName { get; set; }
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public int DiscountCodeId { get; set; }
    public string? DiscountCodeCode { get; set; }
    public bool? DiscountCodeActive { get; set; }
    public DateTimeOffset? DiscountCodeValidFrom { get; set; }
    public DateTimeOffset? DiscountCodeValidTo { get; set; }
    public string? Notes { get; set; }
    public int TimesUsed { get; set; }
    public DateTimeOffset? ValidFrom { get; set; }
    public DateTimeOffset? ExpiresOn { get; set; }
    public string? RefererGuid { get; set; }
    public string? BookingUniqueId { get; set; }
}
