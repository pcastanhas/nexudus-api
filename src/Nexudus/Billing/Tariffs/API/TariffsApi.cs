using Nexudus.Billing.Tariffs.Models;

namespace Nexudus.Billing.Tariffs.API;

/// <summary>Strongly-typed access to the Tariff (plan) endpoints.</summary>
public sealed class TariffsApi : NexudusEndpoint<Tariff>
{
    public TariffsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tariffs";

    public Task<PagedResult<Tariff>> SearchTariffs(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<Tariff?> GetOneTariff(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<Tariff>> GetMultipleTariffs(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariff(Tariff record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariff(Tariff record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariff(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<Tariff> EnumerateTariffs(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
