using Nexudus.Billing.PaymentGateways.Models;

namespace Nexudus.Billing.PaymentGateways.API;

/// <summary>Strongly-typed access to the PaymentGateway endpoints.</summary>
public sealed class PaymentGatewaysApi : NexudusEndpoint<PaymentGateway>
{
    public PaymentGatewaysApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/paymentgateways";

    public Task<PagedResult<PaymentGateway>> SearchPaymentGateways(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<PaymentGateway?> GetOnePaymentGateway(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<PaymentGateway>> GetMultiplePaymentGateways(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreatePaymentGateway(PaymentGateway record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdatePaymentGateway(PaymentGateway record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeletePaymentGateway(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<PaymentGateway> EnumeratePaymentGateways(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
