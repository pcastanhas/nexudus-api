namespace Nexudus.Billing.CoworkerContracts.Models;

/// <summary>
/// The foundation for automatic billing: links a customer (<c>Coworker</c>) to a plan (<c>Tariff</c>)
/// that drives billing frequency, benefits, and defaults. A customer with at least one active contract
/// is a Member; one with none is a Contact.
/// <para>
/// Price can be fixed (<see cref="Price"/> not null) or derived from the plan. <see cref="RenewalDate"/>
/// is the next auto-invoice date; <see cref="InvoicedPeriod"/> is the period the next invoice covers.
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one CoworkerContract" response.
/// </para>
/// </summary>
public sealed class CoworkerContract : NexudusEntity
{
    public int IssuedById { get; set; }
    public string? IssuedByName { get; set; }
    public int CoworkerId { get; set; }
    public string? CoworkerCoworkerType { get; set; }
    public string? CoworkerFullName { get; set; }
    public string? CoworkerCompanyName { get; set; }
    public string? CoworkerBillingName { get; set; }
    public string? CoworkerEmail { get; set; }
    public bool? CoworkerActive { get; set; }
    public int TariffId { get; set; }
    public string? TariffName { get; set; }
    public int? TariffInvoiceEvery { get; set; }
    public int? TariffInvoiceEveryWeeks { get; set; }
    public decimal TariffPrice { get; set; }
    public string? TariffCurrencyCode { get; set; }
    public int? NextTariffId { get; set; }
    public string? NextTariffName { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public int BillingDay { get; set; }
    public DateTimeOffset? RenewalDate { get; set; }
    public DateTimeOffset? InvoicedPeriod { get; set; }
    public DateTimeOffset? ContractTerm { get; set; }
    public decimal? Price { get; set; }
    public decimal? Value { get; set; }
    public List<long>? Desks { get; set; }
    public List<long>? Variants { get; set; }
    public int Quantity { get; set; }
    public bool Active { get; set; }
    public bool MainContract { get; set; }
    public bool Cancelled { get; set; }
    public string? PurchaseOrder { get; set; }
    public bool IncludeSignupFee { get; set; }
    public bool InvoiceAdvancedCycles { get; set; }
    public bool ApplyProRating { get; set; }
    public DateTimeOffset? NextAutoInvoice { get; set; }
    public bool PricePlanTermsAccepted { get; set; }
    public DateTimeOffset? PricePlanTermsAcceptedOn { get; set; }
    public DateTimeOffset? CancellationDate { get; set; }
    public int? CancellationLimitDays { get; set; }
    public bool ProRateCancellation { get; set; }
    public bool CancelTeamContracts { get; set; }
    public int CancellationReason { get; set; }
    public string? CancellationNotes { get; set; }
    public int DeliveryHandlingPreferenceChecks { get; set; }
    public int DeliveryHandlingPreferenceMail { get; set; }
    public int DeliveryHandlingPreferenceParcels { get; set; }
    public int DeliveryHandlingPreferencePublicity { get; set; }
    public string? DeliveryInstructions { get; set; }
    public DateTimeOffset? IdentityChecksDueOn { get; set; }
    public DateTimeOffset? AddressChecksDueOn { get; set; }
    public DateTimeOffset? StartDateLocal { get; set; }
    public DateTimeOffset? RenewalDateLocal { get; set; }
    public DateTimeOffset? NextAutoInvoiceLocal { get; set; }
    public DateTimeOffset? PricePlanTermsAcceptedOnLocal { get; set; }
    public DateTimeOffset? CancellationDateLocal { get; set; }
    public DateTimeOffset? ContractTermLocal { get; set; }
    public string? ProposalUniqueId { get; set; }
    public string? ProposalContractUniqueId { get; set; }
    public string? CourseMemberUniqueId { get; set; }
    public DateTimeOffset? InvoicedPeriodLocal { get; set; }
    public string? FloorPlanDeskIds { get; set; }
    public string? FloorPlanDeskNames { get; set; }
    public string? FloorPlanDeskVariantIds { get; set; }
    public string? FloorPlanDeskVariantNames { get; set; }
    public decimal PriceWithProductsAndDeposits { get; set; }
    public decimal PriceWithProducts { get; set; }
    public string? PoBoxNumber { get; set; }
    public bool InPausedPeriod { get; set; }
    public DateTimeOffset? InPausedPeriodFrom { get; set; }
    public DateTimeOffset? InPausedPeriodUntil { get; set; }
}
