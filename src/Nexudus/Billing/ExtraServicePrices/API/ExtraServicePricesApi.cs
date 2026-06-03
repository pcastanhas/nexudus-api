using Nexudus.Billing.ExtraServicePrices.Models;

namespace Nexudus.Billing.ExtraServicePrices.API;

/// <summary>Strongly-typed access to the ExtraServicePrice endpoints.</summary>
public sealed class ExtraServicePricesApi : NexudusEndpoint<ExtraServicePrice>
{
    public ExtraServicePricesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/extraserviceprices";

    public Task<PagedResult<ExtraServicePrice>> SearchExtraServicePrices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ExtraServicePrice?> GetOneExtraServicePrice(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ExtraServicePrice>> GetMultipleExtraServicePrices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateExtraServicePrice(ExtraServicePrice record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateExtraServicePrice(ExtraServicePrice record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteExtraServicePrice(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ExtraServicePrice> EnumerateExtraServicePrices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
