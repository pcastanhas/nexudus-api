using Nexudus.Billing.ExtraServices.Models;

namespace Nexudus.Billing.ExtraServices.API;

/// <summary>Strongly-typed access to the ExtraService ("booking rate") endpoints.</summary>
public sealed class ExtraServicesApi : NexudusEndpoint<ExtraService>
{
    public ExtraServicesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/extraservices";

    public Task<PagedResult<ExtraService>> SearchExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ExtraService?> GetOneExtraService(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ExtraService>> GetMultipleExtraServices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateExtraService(ExtraService record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateExtraService(ExtraService record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteExtraService(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ExtraService> EnumerateExtraServices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
