namespace Nexudus.Billing.CoworkerPaymentMethods.Models;

/// <summary>
/// A tokenised payment method stored against a customer and a location, used to charge invoices issued to
/// that customer. Supported providers are Stripe (card and ACH/BACS) and GoCardless (direct-debit mandates).
/// <para>
/// For Stripe, <see cref="MethodId"/> holds the payment-method ID and <see cref="CustomerId"/> the Stripe
/// customer ID; for GoCardless, <see cref="MandateId"/> holds the mandate ID. <see cref="CardNumber"/> is a
/// masked number for display only. Fields below the <see cref="NexudusEntity"/> base mirror the
/// "Get one CoworkerPaymentMethod" response.
/// </para>
/// </summary>
public sealed class CoworkerPaymentMethod : NexudusEntity
{
    public int CoworkerId { get; set; }
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? BusinessCurrencyCode { get; set; }
    public PaymentProvider RegularPaymentProvider { get; set; }
    public string? MethodId { get; set; }
    public string? CustomerId { get; set; }
    public string? MandateId { get; set; }
    public string? CardNumber { get; set; }
    public string? Notes { get; set; }
}
