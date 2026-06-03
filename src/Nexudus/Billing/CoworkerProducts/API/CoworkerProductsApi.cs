using Nexudus.Billing.CoworkerProducts.Models;

namespace Nexudus.Billing.CoworkerProducts.API;

/// <summary>Strongly-typed access to the CoworkerProduct endpoints.</summary>
public sealed class CoworkerProductsApi : NexudusEndpoint<CoworkerProduct>
{
    public CoworkerProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerproducts";

    public Task<PagedResult<CoworkerProduct>> SearchCoworkerProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerProduct?> GetOneCoworkerProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerProduct>> GetMultipleCoworkerProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerProduct(CoworkerProduct record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerProduct(CoworkerProduct record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerProduct> EnumerateCoworkerProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
