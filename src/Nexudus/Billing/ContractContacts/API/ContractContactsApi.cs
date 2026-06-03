using Nexudus.Billing.ContractContacts.Models;

namespace Nexudus.Billing.ContractContacts.API;

/// <summary>Strongly-typed access to the ContractContact endpoints.</summary>
public sealed class ContractContactsApi : NexudusEndpoint<ContractContact>
{
    public ContractContactsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/contractcontacts";

    public Task<PagedResult<ContractContact>> SearchContractContacts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ContractContact?> GetOneContractContact(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ContractContact>> GetMultipleContractContacts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateContractContact(ContractContact record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateContractContact(ContractContact record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteContractContact(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ContractContact> EnumerateContractContacts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
