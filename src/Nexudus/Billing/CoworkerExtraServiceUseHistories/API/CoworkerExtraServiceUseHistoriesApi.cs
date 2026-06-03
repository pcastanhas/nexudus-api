using Nexudus.Billing.CoworkerExtraServiceUseHistories.Models;

namespace Nexudus.Billing.CoworkerExtraServiceUseHistories.API;

/// <summary>
/// Strongly-typed access to the CoworkerExtraServiceUseHistory endpoints. This entity is an audit/ledger
/// view; the write methods are exposed for completeness but records are normally created by the system.
/// </summary>
public sealed class CoworkerExtraServiceUseHistoriesApi : NexudusEndpoint<CoworkerExtraServiceUseHistory>
{
    public CoworkerExtraServiceUseHistoriesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerextraserviceusehistories";

    public Task<PagedResult<CoworkerExtraServiceUseHistory>> SearchCoworkerExtraServiceUseHistories(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerExtraServiceUseHistory?> GetOneCoworkerExtraServiceUseHistory(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerExtraServiceUseHistory>> GetMultipleCoworkerExtraServiceUseHistories(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerExtraServiceUseHistory(CoworkerExtraServiceUseHistory record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerExtraServiceUseHistory(CoworkerExtraServiceUseHistory record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerExtraServiceUseHistory(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerExtraServiceUseHistory> EnumerateCoworkerExtraServiceUseHistories(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
