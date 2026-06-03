using Nexudus.Billing.TariffSignupProducts.Models;

namespace Nexudus.Billing.TariffSignupProducts.API;

/// <summary>Strongly-typed access to the TariffSignupProduct endpoints.</summary>
public sealed class TariffSignupProductsApi : NexudusEndpoint<TariffSignupProduct>
{
    public TariffSignupProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tariffsignupproducts";

    public Task<PagedResult<TariffSignupProduct>> SearchTariffSignupProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TariffSignupProduct?> GetOneTariffSignupProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TariffSignupProduct>> GetMultipleTariffSignupProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariffSignupProduct(TariffSignupProduct record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariffSignupProduct(TariffSignupProduct record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariffSignupProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TariffSignupProduct> EnumerateTariffSignupProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
