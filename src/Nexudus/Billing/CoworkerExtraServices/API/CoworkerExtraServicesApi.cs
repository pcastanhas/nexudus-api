using Nexudus.Billing.CoworkerExtraServices.Models;

namespace Nexudus.Billing.CoworkerExtraServices.API;

/// <summary>Strongly-typed access to the CoworkerExtraService endpoints.</summary>
public sealed class CoworkerExtraServicesApi : NexudusEndpoint<CoworkerExtraService>
{
    public CoworkerExtraServicesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerextraservices";

    public Task<PagedResult<CoworkerExtraService>> SearchCoworkerExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerExtraService?> GetOneCoworkerExtraService(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerExtraService>> GetMultipleCoworkerExtraServices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerExtraService(CoworkerExtraService record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerExtraService(CoworkerExtraService record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerExtraService(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerExtraService> EnumerateCoworkerExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
