using Nexudus.Billing.FinancialAccounts.Models;

namespace Nexudus.Billing.FinancialAccounts.API;

/// <summary>Strongly-typed access to the FinancialAccount endpoints.</summary>
public sealed class FinancialAccountsApi : NexudusEndpoint<FinancialAccount>
{
    public FinancialAccountsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/financialaccounts";

    public Task<PagedResult<FinancialAccount>> SearchFinancialAccounts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<FinancialAccount?> GetOneFinancialAccount(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<FinancialAccount>> GetMultipleFinancialAccounts(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateFinancialAccount(FinancialAccount record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateFinancialAccount(FinancialAccount record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteFinancialAccount(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<FinancialAccount> EnumerateFinancialAccounts(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
