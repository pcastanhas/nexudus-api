using Nexudus.Billing.ContractDeposits.Models;

namespace Nexudus.Billing.ContractDeposits.API;

/// <summary>Strongly-typed access to the ContractDeposit endpoints.</summary>
public sealed class ContractDepositsApi : NexudusEndpoint<ContractDeposit>
{
    public ContractDepositsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/contractdeposits";

    public Task<PagedResult<ContractDeposit>> SearchContractDeposits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ContractDeposit?> GetOneContractDeposit(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ContractDeposit>> GetMultipleContractDeposits(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateContractDeposit(ContractDeposit record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateContractDeposit(ContractDeposit record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteContractDeposit(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ContractDeposit> EnumerateContractDeposits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
