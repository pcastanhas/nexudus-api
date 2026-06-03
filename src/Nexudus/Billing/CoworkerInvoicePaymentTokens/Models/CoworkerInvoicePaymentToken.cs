namespace Nexudus.Billing.CoworkerInvoicePaymentTokens.Models;

/// <summary>
/// An internal record of the token used to process payment for a customer invoice, linking a
/// <c>CoworkerInvoice</c> to the payment provider and the provider-specific token string.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one CoworkerInvoicePaymentToken" response.
/// </para>
/// </summary>
public sealed class CoworkerInvoicePaymentToken : NexudusEntity
{
    public int CoworkerInvoiceId { get; set; }
    public PaymentProvider RegularPaymentProvider { get; set; }
    public string? Token { get; set; }
    public string? Notes { get; set; }
}
