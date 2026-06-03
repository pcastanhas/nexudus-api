using Nexudus.Billing.ContractSchedules.Models;

namespace Nexudus.Billing.ContractSchedules.API;

/// <summary>Strongly-typed access to the ContractSchedule endpoints.</summary>
public sealed class ContractSchedulesApi : NexudusEndpoint<ContractSchedule>
{
    public ContractSchedulesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/contractschedules";

    public Task<PagedResult<ContractSchedule>> SearchContractSchedules(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ContractSchedule?> GetOneContractSchedule(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ContractSchedule>> GetMultipleContractSchedules(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateContractSchedule(ContractSchedule record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateContractSchedule(ContractSchedule record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteContractSchedule(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ContractSchedule> EnumerateContractSchedules(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
