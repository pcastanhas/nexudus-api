using Nexudus.Billing.TimePassPrices.Models;

namespace Nexudus.Billing.TimePassPrices.API;

/// <summary>Strongly-typed access to the TimePassPrice endpoints.</summary>
public sealed class TimePassPricesApi : NexudusEndpoint<TimePassPrice>
{
    public TimePassPricesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/timepassprices";

    public Task<PagedResult<TimePassPrice>> SearchTimePassPrices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TimePassPrice?> GetOneTimePassPrice(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TimePassPrice>> GetMultipleTimePassPrices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTimePassPrice(TimePassPrice record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTimePassPrice(TimePassPrice record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTimePassPrice(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TimePassPrice> EnumerateTimePassPrices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
