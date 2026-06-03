using Nexudus.Billing.BusinessCharges.Models;

namespace Nexudus.Billing.BusinessCharges.API;

/// <summary>Strongly-typed access to the BusinessCharge endpoints.</summary>
public sealed class BusinessChargesApi : NexudusEndpoint<BusinessCharge>
{
    public BusinessChargesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/businesscharges";

    public Task<PagedResult<BusinessCharge>> SearchBusinessCharges(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<BusinessCharge?> GetOneBusinessCharge(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<BusinessCharge>> GetMultipleBusinessCharges(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateBusinessCharge(BusinessCharge record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateBusinessCharge(BusinessCharge record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteBusinessCharge(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<BusinessCharge> EnumerateBusinessCharges(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
