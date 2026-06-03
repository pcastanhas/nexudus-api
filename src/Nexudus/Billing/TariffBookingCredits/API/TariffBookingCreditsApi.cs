using Nexudus.Billing.TariffBookingCredits.Models;

namespace Nexudus.Billing.TariffBookingCredits.API;

/// <summary>Strongly-typed access to the TariffBookingCredit endpoints.</summary>
public sealed class TariffBookingCreditsApi : NexudusEndpoint<TariffBookingCredit>
{
    public TariffBookingCreditsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tariffbookingcredits";

    public Task<PagedResult<TariffBookingCredit>> SearchTariffBookingCredits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TariffBookingCredit?> GetOneTariffBookingCredit(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TariffBookingCredit>> GetMultipleTariffBookingCredits(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariffBookingCredit(TariffBookingCredit record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariffBookingCredit(TariffBookingCredit record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariffBookingCredit(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TariffBookingCredit> EnumerateTariffBookingCredits(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
