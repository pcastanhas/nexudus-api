using Nexudus.Billing.ProductBookingCredits.Models;

namespace Nexudus.Billing.ProductBookingCredits.API;

/// <summary>Strongly-typed access to the ProductBookingCredit endpoints.</summary>
public sealed class ProductBookingCreditsApi : NexudusEndpoint<ProductBookingCredit>
{
    public ProductBookingCreditsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/productbookingcredits";

    public Task<PagedResult<ProductBookingCredit>> SearchProductBookingCredits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProductBookingCredit?> GetOneProductBookingCredit(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProductBookingCredit>> GetMultipleProductBookingCredits(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProductBookingCredit(ProductBookingCredit record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProductBookingCredit(ProductBookingCredit record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProductBookingCredit(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProductBookingCredit> EnumerateProductBookingCredits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
