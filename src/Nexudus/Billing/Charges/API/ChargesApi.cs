using Nexudus.Billing.Charges.Models;

namespace Nexudus.Billing.Charges.API;

/// <summary>
/// Strongly-typed access to the Charge endpoints. Construct with a configured
/// <see cref="NexudusClient"/>: <c>var charges = new ChargesApi(client);</c>
/// </summary>
public sealed class ChargesApi : NexudusEndpoint<Charge>
{
    public ChargesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/charges";

    /// <summary>Search/list charges (one page) with optional pagination, sorting, and filters.</summary>
    public Task<PagedResult<Charge>> SearchCharges(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    /// <summary>Search/list charges using the strongly-typed <see cref="ChargeFilter"/>.</summary>
    public Task<PagedResult<Charge>> SearchCharges(ChargeFilter filter, CancellationToken cancellationToken = default)
        => SearchAsync(filter, cancellationToken);

    /// <summary>Retrieve a single, fully-populated charge by Id (null if not found).</summary>
    public Task<Charge?> GetOneCharge(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    /// <summary>Retrieve several charges by Id in one request.</summary>
    public Task<IReadOnlyList<Charge>> GetMultipleCharges(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    /// <summary>Retrieve several charges by Id in one request.</summary>
    public Task<IReadOnlyList<Charge>> GetMultipleCharges(params long[] ids)
        => GetManyAsync(ids, CancellationToken.None);

    /// <summary>Create a charge. Returns the new charge's Id.</summary>
    public Task<long> CreateCharge(Charge charge, CancellationToken cancellationToken = default)
        => CreateAsync(charge, cancellationToken);

    /// <summary>
    /// Update a charge. Pass a complete record fetched via <see cref="GetOneCharge"/>;
    /// the API has no PATCH, so omitted fields are cleared.
    /// </summary>
    public Task<CommandResult> UpdateCharge(Charge charge, CancellationToken cancellationToken = default)
        => UpdateAsync(charge, cancellationToken);

    /// <summary>Delete a charge by Id.</summary>
    public Task<CommandResult> DeleteCharge(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    /// <summary>Stream every matching charge, following pagination automatically.</summary>
    public IAsyncEnumerable<Charge> EnumerateCharges(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
