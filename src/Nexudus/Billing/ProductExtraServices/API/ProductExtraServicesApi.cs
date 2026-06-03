using Nexudus.Billing.ProductExtraServices.Models;

namespace Nexudus.Billing.ProductExtraServices.API;

/// <summary>Strongly-typed access to the ProductExtraService endpoints.</summary>
public sealed class ProductExtraServicesApi : NexudusEndpoint<ProductExtraService>
{
    public ProductExtraServicesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/productextraservices";

    public Task<PagedResult<ProductExtraService>> SearchProductExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProductExtraService?> GetOneProductExtraService(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProductExtraService>> GetMultipleProductExtraServices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProductExtraService(ProductExtraService record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProductExtraService(ProductExtraService record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProductExtraService(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProductExtraService> EnumerateProductExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
