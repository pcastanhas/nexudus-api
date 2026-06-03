using Nexudus.Billing.CoworkerPaymentMethods.Models;

namespace Nexudus.Billing.CoworkerPaymentMethods.API;

/// <summary>Strongly-typed access to the CoworkerPaymentMethod endpoints.</summary>
public sealed class CoworkerPaymentMethodsApi : NexudusEndpoint<CoworkerPaymentMethod>
{
    public CoworkerPaymentMethodsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerpaymentmethods";

    public Task<PagedResult<CoworkerPaymentMethod>> SearchCoworkerPaymentMethods(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerPaymentMethod?> GetOneCoworkerPaymentMethod(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerPaymentMethod>> GetMultipleCoworkerPaymentMethods(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerPaymentMethod(CoworkerPaymentMethod record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerPaymentMethod(CoworkerPaymentMethod record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerPaymentMethod(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerPaymentMethod> EnumerateCoworkerPaymentMethods(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
