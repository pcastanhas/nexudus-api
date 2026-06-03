using Nexudus.Billing.CoworkerBookingCreditUseHistories.Models;

namespace Nexudus.Billing.CoworkerBookingCreditUseHistories.API;

/// <summary>
/// Strongly-typed access to the CoworkerBookingCreditUseHistory endpoints. The API exposes search, get,
/// create, and update for this entity but no delete endpoint, so no delete wrapper is provided.
/// </summary>
public sealed class CoworkerBookingCreditUseHistoriesApi : NexudusEndpoint<CoworkerBookingCreditUseHistory>
{
    public CoworkerBookingCreditUseHistoriesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerbookingcreditusehistories";

    public Task<PagedResult<CoworkerBookingCreditUseHistory>> SearchCoworkerBookingCreditUseHistories(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerBookingCreditUseHistory?> GetOneCoworkerBookingCreditUseHistory(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerBookingCreditUseHistory>> GetMultipleCoworkerBookingCreditUseHistories(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerBookingCreditUseHistory(CoworkerBookingCreditUseHistory record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerBookingCreditUseHistory(CoworkerBookingCreditUseHistory record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public IAsyncEnumerable<CoworkerBookingCreditUseHistory> EnumerateCoworkerBookingCreditUseHistories(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
