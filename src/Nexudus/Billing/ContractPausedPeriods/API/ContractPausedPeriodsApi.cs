using Nexudus.Billing.ContractPausedPeriods.Models;

namespace Nexudus.Billing.ContractPausedPeriods.API;

/// <summary>Strongly-typed access to the ContractPausedPeriod endpoints.</summary>
public sealed class ContractPausedPeriodsApi : NexudusEndpoint<ContractPausedPeriod>
{
    public ContractPausedPeriodsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/contractpausedperiods";

    public Task<PagedResult<ContractPausedPeriod>> SearchContractPausedPeriods(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ContractPausedPeriod?> GetOneContractPausedPeriod(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ContractPausedPeriod>> GetMultipleContractPausedPeriods(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateContractPausedPeriod(ContractPausedPeriod record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateContractPausedPeriod(ContractPausedPeriod record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteContractPausedPeriod(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ContractPausedPeriod> EnumerateContractPausedPeriods(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
