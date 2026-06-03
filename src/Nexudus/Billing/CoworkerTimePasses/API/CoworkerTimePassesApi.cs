using Nexudus.Billing.CoworkerTimePasses.Models;

namespace Nexudus.Billing.CoworkerTimePasses.API;

/// <summary>Strongly-typed access to the CoworkerTimePass endpoints.</summary>
public sealed class CoworkerTimePassesApi : NexudusEndpoint<CoworkerTimePass>
{
    public CoworkerTimePassesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkertimepasses";

    public Task<PagedResult<CoworkerTimePass>> SearchCoworkerTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerTimePass?> GetOneCoworkerTimePass(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerTimePass>> GetMultipleCoworkerTimePasses(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerTimePass(CoworkerTimePass record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerTimePass(CoworkerTimePass record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerTimePass(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerTimePass> EnumerateCoworkerTimePasses(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
