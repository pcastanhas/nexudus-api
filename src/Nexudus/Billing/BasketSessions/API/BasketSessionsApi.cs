using Nexudus.Billing.BasketSessions.Models;

namespace Nexudus.Billing.BasketSessions.API;

/// <summary>Strongly-typed access to the BasketSession endpoints.</summary>
public sealed class BasketSessionsApi : NexudusEndpoint<BasketSession>
{
    public BasketSessionsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/basketsessions";

    public Task<PagedResult<BasketSession>> SearchBasketSessions(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<BasketSession?> GetOneBasketSession(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<BasketSession>> GetMultipleBasketSessions(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateBasketSession(BasketSession record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateBasketSession(BasketSession record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteBasketSession(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<BasketSession> EnumerateBasketSessions(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
