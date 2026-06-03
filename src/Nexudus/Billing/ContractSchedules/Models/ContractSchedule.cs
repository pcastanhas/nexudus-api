namespace Nexudus.Billing.ContractSchedules.Models;

/// <summary>
/// A future price change for a plan contract (<c>CoworkerContract</c>). On <see cref="ApplyOn"/> the system
/// automatically updates the contract's price to <see cref="Price"/>.
/// <para>
/// Useful for stepped pricing (e.g. an introductory rate that transitions to a full rate on a known date).
/// Once processed, <see cref="Applied"/> becomes true and the record is read-only. Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one ContractSchedule" response.
/// </para>
/// </summary>
public sealed class ContractSchedule : NexudusEntity
{
    public int CoworkerContractId { get; set; }
    public int? CoworkerContractQuantity { get; set; }
    public string? CoworkerContractFloorPlanDeskIds { get; set; }
    public string? CoworkerContractFloorPlanDeskNames { get; set; }
    public string? CoworkerContractTariffName { get; set; }
    public int? CoworkerContractCoworkerId { get; set; }
    public string? CoworkerContractCoworkerFullName { get; set; }
    public string? CoworkerContractCoworkerBillingName { get; set; }
    public string? Notes { get; set; }
    public decimal? Price { get; set; }
    public DateTimeOffset? ApplyOn { get; set; }
    public DateTimeOffset? ApplyOnLocal { get; set; }
    public bool Applied { get; set; }
}
