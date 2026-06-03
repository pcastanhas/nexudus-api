using Nexudus.Billing.ContractProducts.Models;

namespace Nexudus.Billing.ContractProducts.API;

/// <summary>Strongly-typed access to the ContractProduct endpoints.</summary>
public sealed class ContractProductsApi : NexudusEndpoint<ContractProduct>
{
    public ContractProductsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/contractproducts";

    public Task<PagedResult<ContractProduct>> SearchContractProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ContractProduct?> GetOneContractProduct(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ContractProduct>> GetMultipleContractProducts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateContractProduct(ContractProduct record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateContractProduct(ContractProduct record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteContractProduct(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ContractProduct> EnumerateContractProducts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
