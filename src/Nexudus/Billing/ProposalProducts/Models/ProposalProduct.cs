namespace Nexudus.Billing.ProposalProducts.Models;

/// <summary>
/// Links a product to a <c>Proposal</c>, defining an additional item or service included in the offer with
/// its pricing and recurrence settings.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one ProposalProduct" response. The
/// denormalised <see cref="ProposalCoworkerId"/> is returned by this endpoint as a string.
/// </para>
/// </summary>
public sealed class ProposalProduct : NexudusEntity
{
    public int ProposalId { get; set; }
    public string? ProposalCoworkerId { get; set; }
    public string? ProposalCoworkerEmail { get; set; }
    public string? ProposalCoworkerFullName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public bool? ProductApplyProRating { get; set; }
    public string? ProductBusinessCurrencyCode { get; set; }
    public int Quantity { get; set; }
    public decimal? Price { get; set; }
    public bool IsDeposit { get; set; }
    public bool IsContractProduct { get; set; }
    public bool RegularCharge { get; set; }
    public RecurrentChargePattern RepeatCycle { get; set; }
    public DateTimeOffset? InvoiceOn { get; set; }
    public DateTimeOffset? RepeatFrom { get; set; }
    public DateTimeOffset? RepeatUntil { get; set; }
    public int? RepeatUnit { get; set; }
    public bool ApplyProRating { get; set; }
    public string? Notes { get; set; }
}
