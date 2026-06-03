using Nexudus.Billing.CoworkerBookingCredits.Models;

namespace Nexudus.Billing.CoworkerBookingCredits.API;

/// <summary>Strongly-typed access to the CoworkerBookingCredit endpoints.</summary>
public sealed class CoworkerBookingCreditsApi : NexudusEndpoint<CoworkerBookingCredit>
{
    public CoworkerBookingCreditsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerbookingcredits";

    public Task<PagedResult<CoworkerBookingCredit>> SearchCoworkerBookingCredits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerBookingCredit?> GetOneCoworkerBookingCredit(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerBookingCredit>> GetMultipleCoworkerBookingCredits(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerBookingCredit(CoworkerBookingCredit record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerBookingCredit(CoworkerBookingCredit record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerBookingCredit(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerBookingCredit> EnumerateCoworkerBookingCredits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
