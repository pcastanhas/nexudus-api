using Nexudus.Billing.TariffExtraServices.Models;

namespace Nexudus.Billing.TariffExtraServices.API;

/// <summary>Strongly-typed access to the TariffExtraService endpoints.</summary>
public sealed class TariffExtraServicesApi : NexudusEndpoint<TariffExtraService>
{
    public TariffExtraServicesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tariffextraservices";

    public Task<PagedResult<TariffExtraService>> SearchTariffExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TariffExtraService?> GetOneTariffExtraService(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TariffExtraService>> GetMultipleTariffExtraServices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariffExtraService(TariffExtraService record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariffExtraService(TariffExtraService record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariffExtraService(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TariffExtraService> EnumerateTariffExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
