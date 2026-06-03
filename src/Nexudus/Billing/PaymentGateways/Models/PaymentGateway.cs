namespace Nexudus.Billing.PaymentGateways.Models;

/// <summary>A connection to a supported payment provider used to process card payments.</summary>
public sealed class PaymentGateway : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? Name { get; set; }

    /// <summary>
    /// The payment provider (<c>ePaymentGatewayType</c>). Kept as an integer because the enum has
    /// ~200 members; common values include 13 = stripe, 320 = stripe_payment_intents, 201 = adyen,
    /// 4 = braintree, 49 = paypal. See the API docs for the full list.
    /// </summary>
    public int PaymentGatewayType { get; set; }

    public bool Attempt3dSecure { get; set; }

    /// <summary>API access token / secret key for the provider.</summary>
    public string? AccessToken { get; set; }

    /// <summary>Provider-specific configuration in XML form.</summary>
    public string? ConfigurationXml { get; set; }

    /// <summary>Last XML response returned by the provider.</summary>
    public string? XmlResponse { get; set; }

    public decimal? TransactionFee { get; set; }
    public int? FinancialAccountId { get; set; }
    public int? TaxRateId { get; set; }
}
