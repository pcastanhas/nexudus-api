using Nexudus.Billing.ProposalContracts.Models;

namespace Nexudus.Billing.ProposalContracts.API;

/// <summary>Strongly-typed access to the ProposalContract endpoints.</summary>
public sealed class ProposalContractsApi : NexudusEndpoint<ProposalContract>
{
    public ProposalContractsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/proposalcontracts";

    public Task<PagedResult<ProposalContract>> SearchProposalContracts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProposalContract?> GetOneProposalContract(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProposalContract>> GetMultipleProposalContracts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProposalContract(ProposalContract record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProposalContract(ProposalContract record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProposalContract(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProposalContract> EnumerateProposalContracts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
