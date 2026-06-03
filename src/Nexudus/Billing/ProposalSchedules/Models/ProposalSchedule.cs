namespace Nexudus.Billing.ProposalSchedules.Models;

/// <summary>
/// A scheduled price change at the proposal level: on <see cref="ApplyOn"/> the price is set to
/// <see cref="Price"/>.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one ProposalSchedule" response; the
/// denormalised <see cref="ProposalQuantity"/> and <see cref="ProposalCoworkerId"/> are returned by this
/// endpoint as strings.
/// </para>
/// </summary>
public sealed class ProposalSchedule : NexudusEntity
{
    public int ProposalId { get; set; }
    public string? ProposalQuantity { get; set; }
    public string? ProposalTariffName { get; set; }
    public string? ProposalCoworkerId { get; set; }
    public string? ProposalCoworkerFullName { get; set; }
    public string? ProposalCoworkerBillingName { get; set; }
    public string? Notes { get; set; }
    public decimal? Price { get; set; }
    public DateTimeOffset? ApplyOn { get; set; }
    public bool Applied { get; set; }
}
