namespace Nexudus.Billing.ProposalContracts.Models;

/// <summary>
/// Links a contract configuration to a <c>Proposal</c>, defining the plan and terms applied if the proposal
/// is accepted. Carries the same contract properties as a <c>CoworkerContract</c>.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one ProposalContract" response. The
/// denormalised <see cref="TariffInvoiceEvery"/>/<see cref="TariffInvoiceEveryWeeks"/> and
/// <see cref="ProposalCoworkerId"/> are returned by this endpoint as strings.
/// </para>
/// </summary>
public sealed class ProposalContract : NexudusEntity
{
    public int ProposalId { get; set; }
    public string? ProposalCoworkerId { get; set; }
    public string? ProposalCoworkerEmail { get; set; }
    public string? ProposalCoworkerFullName { get; set; }
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public string? TariffInvoiceEvery { get; set; }
    public string? TariffInvoiceEveryWeeks { get; set; }
    public decimal TariffPrice { get; set; }
    public string? TariffBusinessCurrencyCode { get; set; }
    public List<long>? Desks { get; set; }
    public List<long>? Variants { get; set; }
    public decimal? Price { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public int? CancellationLimitDays { get; set; }
    public DateTimeOffset? ContractTerm { get; set; }
    public DateTimeOffset? CancellationDate { get; set; }
    public int BillingDay { get; set; }
    public int Quantity { get; set; }
    public int? DiscountCodeId { get; set; }
    public string? FloorPlanDeskIds { get; set; }
    public string? FloorPlanDeskNames { get; set; }
    public string? FloorPlanDeskVariantIds { get; set; }
    public string? FloorPlanDeskVariantNames { get; set; }
}
