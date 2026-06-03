namespace Nexudus.Billing.ContractContacts.Models;

/// <summary>
/// A key contact on a virtual-office contract (<c>CoworkerContract</c>) — a director, company alias,
/// or nominated recipient used to identify, validate, and handle mail addressed to the company.
/// <para>
/// Link an existing member via <see cref="CoworkerId"/> (name/email are then resolved automatically),
/// or supply <see cref="FullName"/> and <see cref="Email"/> directly for contacts without a member record.
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one ContractContact" response.
/// </para>
/// </summary>
public sealed class ContractContact : NexudusEntity
{
    public int CoworkerContractId { get; set; }
    public int? CoworkerContractQuantity { get; set; }
    public string? CoworkerContractFloorPlanDeskIds { get; set; }
    public string? CoworkerContractFloorPlanDeskNames { get; set; }
    public string? CoworkerContractTariffName { get; set; }
    public int? CoworkerId { get; set; }
    public string? CoworkerFullName { get; set; }
    public string? CoworkerCompanyName { get; set; }
    public string? CoworkerBillingName { get; set; }
    public string? CoworkerEmail { get; set; }
    public bool? CoworkerActive { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? PostCode { get; set; }
    public string? CityName { get; set; }
    public string? State { get; set; }
    public int? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Notes { get; set; }
    public ContractContactType ContractContactType { get; set; }
    public AmlCheckStatus AmlCheckStatus { get; set; }
    public DateTimeOffset? AmlCheckDate { get; set; }
    public decimal? AmlOpenSanctionsScore { get; set; }
    public string? AmlOpenSanctionsResponse { get; set; }
    public string? AmlPappersResponse { get; set; }
    public string? AmlPappersStatus { get; set; }
    public string? AmlNotes { get; set; }
    public string? AmlClearedBy { get; set; }
    public DateTimeOffset? AmlClearedOn { get; set; }
}
