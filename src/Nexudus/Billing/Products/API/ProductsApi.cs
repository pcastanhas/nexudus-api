using Nexudus.Billing.Products.Models;

namespace Nexudus.Billing.Products.API;

/// <summary>Strongly-typed access to the Product endpoints.</summary>
public sealed class ProductsApi : NexudusEndpoint<Product>
{
    public ProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/products";

    public Task<PagedResult<Product>> SearchProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<Product?> GetOneProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<Product>> GetMultipleProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProduct(Product record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProduct(Product record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<Product> EnumerateProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
