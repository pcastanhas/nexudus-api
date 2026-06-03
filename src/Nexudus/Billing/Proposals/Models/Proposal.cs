namespace Nexudus.Billing.Proposals.Models;

/// <summary>
/// Bundles one or more contracts (<c>ProposalContract</c>) presented to a customer for review and
/// acceptance; each contract becomes a <c>CoworkerContract</c> when the proposal is accepted.
/// <para>
/// On creation a <c>ProposalContract</c> is created automatically and the contract-related fields here
/// (<see cref="TariffId"/>, <see cref="Desks"/>, <see cref="Variants"/>, <see cref="Price"/>,
/// <see cref="StartDate"/>, <see cref="CancellationLimitDays"/>, <see cref="ContractTerm"/>,
/// <see cref="CancellationDate"/>, <see cref="ExpirationDate"/>, <see cref="BillingDay"/>,
/// <see cref="Quantity"/>) become create-only — edit them afterwards via the associated
/// <c>ProposalContract</c>. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one Proposal" response.
/// </para>
/// </summary>
public sealed class Proposal : NexudusEntity
{
    public int IssuedById { get; set; }
    public string? IssuedByName { get; set; }
    public string? IssuedByCurrencyCode { get; set; }
    public int ResponsibleId { get; set; }
    public string? ResponsibleFullName { get; set; }
    public int CoworkerId { get; set; }
    public string? CoworkerCoworkerType { get; set; }
    public string? CoworkerFullName { get; set; }
    public string? CoworkerCompanyName { get; set; }
    public string? CoworkerBillingName { get; set; }
    public string? Reference { get; set; }
    public string? Notes { get; set; }
    public ProposalStatus ProposalStatus { get; set; }
    public int? DocumentToSendId { get; set; }
    public int? DocumentToSignId { get; set; }
    public string? DocumentToSignHtml { get; set; }
    public string? DocumentToSignBinaryDocumentFileName { get; set; }
    public string? NewDocumentToSignBinaryDocumentUrl { get; set; }
    public bool? ClearDocumentToSignBinaryDocumentFile { get; set; }
    public string? DocumentToSendHtml { get; set; }
    public string? DocumentToSendBinaryDocumentFileName { get; set; }
    public string? NewDocumentToSendBinaryDocumentUrl { get; set; }
    public bool? ClearDocumentToSendBinaryDocumentFile { get; set; }
    public string? ProposalFileFileName { get; set; }
    public string? NewProposalFileUrl { get; set; }
    public bool? ClearProposalFileFile { get; set; }
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
    public DateTimeOffset? ExpirationDate { get; set; }
    public int BillingDay { get; set; }
    public int Quantity { get; set; }
    public int? DiscountCodeId { get; set; }
    public DateTimeOffset? StartDateLocal { get; set; }
    public DateTimeOffset? SentOn { get; set; }
    public DateTimeOffset? SentOnLocal { get; set; }
    public DateTimeOffset? CustomerLastOpenedDate { get; set; }
    public bool DoNotIssueInvoice { get; set; }
}
