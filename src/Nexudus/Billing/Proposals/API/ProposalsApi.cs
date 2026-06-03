using Nexudus.Billing.Proposals.Models;

namespace Nexudus.Billing.Proposals.API;

/// <summary>Strongly-typed access to the Proposal endpoints.</summary>
public sealed class ProposalsApi : NexudusEndpoint<Proposal>
{
    public ProposalsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/proposals";

    public Task<PagedResult<Proposal>> SearchProposals(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<Proposal?> GetOneProposal(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<Proposal>> GetMultipleProposals(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProposal(Proposal record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProposal(Proposal record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProposal(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<Proposal> EnumerateProposals(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
