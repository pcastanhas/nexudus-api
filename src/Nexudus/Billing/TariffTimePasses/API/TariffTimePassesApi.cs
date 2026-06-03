using Nexudus.Billing.TariffTimePasses.Models;

namespace Nexudus.Billing.TariffTimePasses.API;

/// <summary>Strongly-typed access to the TariffTimePass endpoints.</summary>
public sealed class TariffTimePassesApi : NexudusEndpoint<TariffTimePass>
{
    public TariffTimePassesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tarifftimepasses";

    public Task<PagedResult<TariffTimePass>> SearchTariffTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TariffTimePass?> GetOneTariffTimePass(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TariffTimePass>> GetMultipleTariffTimePasses(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariffTimePass(TariffTimePass record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariffTimePass(TariffTimePass record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariffTimePass(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TariffTimePass> EnumerateTariffTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
