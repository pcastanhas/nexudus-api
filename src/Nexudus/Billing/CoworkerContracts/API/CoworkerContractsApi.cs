using Nexudus.Billing.CoworkerContracts.Models;

namespace Nexudus.Billing.CoworkerContracts.API;

/// <summary>Strongly-typed access to the CoworkerContract endpoints.</summary>
public sealed class CoworkerContractsApi : NexudusEndpoint<CoworkerContract>
{
    public CoworkerContractsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkercontracts";

    public Task<PagedResult<CoworkerContract>> SearchCoworkerContracts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerContract?> GetOneCoworkerContract(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerContract>> GetMultipleCoworkerContracts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerContract(CoworkerContract record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerContract(CoworkerContract record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerContract(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerContract> EnumerateCoworkerContracts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
