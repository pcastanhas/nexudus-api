namespace Nexudus.Billing.ContractPausedPeriods.Models;

/// <summary>
/// A freeze period applied to a plan contract (<c>CoworkerContract</c>), suspending it for one or more
/// billing cycles without cancelling it.
/// <para>
/// Freeze dates align to billing-cycle boundaries: <see cref="PauseFrom"/> is the first day of the next
/// billing cycle and <see cref="PauseUntil"/> the first day of the cycle when the plan restarts. No charges
/// are generated for the contract during the freeze. Fields below the <see cref="NexudusEntity"/> base
/// mirror the "Get one ContractPausedPeriod" response.
/// </para>
/// </summary>
public sealed class ContractPausedPeriod : NexudusEntity
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
    public DateTimeOffset? PauseFrom { get; set; }
    public DateTimeOffset? PauseUntil { get; set; }
    public DateTimeOffset? PauseFromLocal { get; set; }
    public DateTimeOffset? PauseUntilLocal { get; set; }
}
