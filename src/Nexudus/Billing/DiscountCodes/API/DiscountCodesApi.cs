using Nexudus.Billing.DiscountCodes.Models;

namespace Nexudus.Billing.DiscountCodes.API;

/// <summary>Strongly-typed access to the DiscountCode endpoints.</summary>
public sealed class DiscountCodesApi : NexudusEndpoint<DiscountCode>
{
    public DiscountCodesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/discountcodes";

    public Task<PagedResult<DiscountCode>> SearchDiscountCodes(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<DiscountCode?> GetOneDiscountCode(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<DiscountCode>> GetMultipleDiscountCodes(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateDiscountCode(DiscountCode record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateDiscountCode(DiscountCode record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteDiscountCode(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<DiscountCode> EnumerateDiscountCodes(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
