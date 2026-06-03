using Nexudus.Billing.ResourceProducts.Models;

namespace Nexudus.Billing.ResourceProducts.API;

/// <summary>Strongly-typed access to the ResourceProduct endpoints.</summary>
public sealed class ResourceProductsApi : NexudusEndpoint<ResourceProduct>
{
    public ResourceProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/resourceproducts";

    public Task<PagedResult<ResourceProduct>> SearchResourceProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ResourceProduct?> GetOneResourceProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ResourceProduct>> GetMultipleResourceProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateResourceProduct(ResourceProduct record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateResourceProduct(ResourceProduct record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteResourceProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ResourceProduct> EnumerateResourceProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
