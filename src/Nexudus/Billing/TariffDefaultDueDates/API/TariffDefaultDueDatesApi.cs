using Nexudus.Billing.TariffDefaultDueDates.Models;

namespace Nexudus.Billing.TariffDefaultDueDates.API;

/// <summary>Strongly-typed access to the TariffDefaultDueDate endpoints.</summary>
public sealed class TariffDefaultDueDatesApi : NexudusEndpoint<TariffDefaultDueDate>
{
    public TariffDefaultDueDatesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/tariffdefaultduedates";

    public Task<PagedResult<TariffDefaultDueDate>> SearchTariffDefaultDueDates(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TariffDefaultDueDate?> GetOneTariffDefaultDueDate(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TariffDefaultDueDate>> GetMultipleTariffDefaultDueDates(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTariffDefaultDueDate(TariffDefaultDueDate record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTariffDefaultDueDate(TariffDefaultDueDate record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTariffDefaultDueDate(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TariffDefaultDueDate> EnumerateTariffDefaultDueDates(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
