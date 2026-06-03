using Nexudus.Billing.ProductTimePasses.Models;

namespace Nexudus.Billing.ProductTimePasses.API;

/// <summary>Strongly-typed access to the ProductTimePass endpoints.</summary>
public sealed class ProductTimePassesApi : NexudusEndpoint<ProductTimePass>
{
    public ProductTimePassesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/producttimepasses";

    public Task<PagedResult<ProductTimePass>> SearchProductTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProductTimePass?> GetOneProductTimePass(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProductTimePass>> GetMultipleProductTimePasses(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProductTimePass(ProductTimePass record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProductTimePass(ProductTimePass record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProductTimePass(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProductTimePass> EnumerateProductTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
