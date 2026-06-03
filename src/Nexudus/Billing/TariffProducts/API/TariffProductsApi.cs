using Nexudus.Billing.TariffProducts.Models;

namespace Nexudus.Billing.TariffProducts.API;

/// <summary>Strongly-typed access to the TariffProduct endpoints.</summary>
public sealed class TariffProductsApi : NexudusEndpoint<TariffProduct>
{
    public TariffProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tariffproducts";

    public Task<PagedResult<TariffProduct>> SearchTariffProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TariffProduct?> GetOneTariffProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TariffProduct>> GetMultipleTariffProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariffProduct(TariffProduct record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariffProduct(TariffProduct record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariffProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TariffProduct> EnumerateTariffProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
