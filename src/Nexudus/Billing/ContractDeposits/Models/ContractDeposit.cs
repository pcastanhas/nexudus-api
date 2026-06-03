namespace Nexudus.Billing.ContractDeposits.Models;

/// <summary>
/// A security deposit or retainer on a plan contract (<c>CoworkerContract</c>), based on a <c>Product</c>.
/// <para>
/// Created automatically when a contract is signed for a plan whose <c>TariffSignupProducts</c> include
/// deposits. When <see cref="Refundable"/> is true, cancelling the parent contract generates a credit note
/// for the deposit amount. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one ContractDeposit" response.
/// </para>
/// </summary>
public sealed class ContractDeposit : NexudusEntity
{
    public int CoworkerContractId { get; set; }
    public int? CoworkerContractQuantity { get; set; }
    public string? CoworkerContractFloorPlanDeskIds { get; set; }
    public string? CoworkerContractFloorPlanDeskNames { get; set; }
    public string? CoworkerContractTariffName { get; set; }
    public int? CoworkerContractCoworkerId { get; set; }
    public string? CoworkerContractCoworkerFullName { get; set; }
    public string? CoworkerContractCoworkerBillingName { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string? ProductCurrencyCode { get; set; }
    public string? Notes { get; set; }
    public decimal? Price { get; set; }
    public bool Refundable { get; set; }
    public bool Invoiced { get; set; }
    public bool Credited { get; set; }
    public DateTimeOffset? InvoicedOn { get; set; }
    public bool InvoiceDuringOnlineCheckout { get; set; }
}
