namespace Nexudus.Billing.ProposalContractSchedules.Models;

/// <summary>
/// A scheduled price change for a contract within a proposal: on <see cref="ApplyOn"/> the contract price is
/// set to <see cref="Price"/>. The proposal-family analogue of <c>ContractSchedule</c>.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one ProposalContractSchedule" response;
/// the denormalised <see cref="ProposalContractQuantity"/> and <see cref="ProposalContractProposalCoworkerId"/>
/// are returned by this endpoint as strings.
/// </para>
/// </summary>
public sealed class ProposalContractSchedule : NexudusEntity
{
    public int ProposalContractId { get; set; }
    public string? ProposalContractQuantity { get; set; }
    public string? ProposalContractTariffName { get; set; }
    public string? ProposalContractProposalCoworkerId { get; set; }
    public string? ProposalContractProposalCoworkerFullName { get; set; }
    public string? ProposalContractProposalCoworkerBillingName { get; set; }
    public string? Notes { get; set; }
    public decimal? Price { get; set; }
    public DateTimeOffset? ApplyOn { get; set; }
    public bool Applied { get; set; }
}
