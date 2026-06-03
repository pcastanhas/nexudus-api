using Nexudus.Billing.TimePasses.Models;

namespace Nexudus.Billing.TimePasses.API;

/// <summary>Strongly-typed access to the TimePass endpoints.</summary>
public sealed class TimePassesApi : NexudusEndpoint<TimePass>
{
    public TimePassesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/timepasses";

    public Task<PagedResult<TimePass>> SearchTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TimePass?> GetOneTimePass(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TimePass>> GetMultipleTimePasses(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTimePass(TimePass record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTimePass(TimePass record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTimePass(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TimePass> EnumerateTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
