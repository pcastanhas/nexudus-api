using Nexudus.Billing.ProposalProducts.Models;

namespace Nexudus.Billing.ProposalProducts.API;

/// <summary>Strongly-typed access to the ProposalProduct endpoints.</summary>
public sealed class ProposalProductsApi : NexudusEndpoint<ProposalProduct>
{
    public ProposalProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/proposalproducts";

    public Task<PagedResult<ProposalProduct>> SearchProposalProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProposalProduct?> GetOneProposalProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProposalProduct>> GetMultipleProposalProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProposalProduct(ProposalProduct record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProposalProduct(ProposalProduct record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProposalProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProposalProduct> EnumerateProposalProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
