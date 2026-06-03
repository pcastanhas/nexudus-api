using Nexudus.Billing.CoworkerInvoiceHistories.Models;

namespace Nexudus.Billing.CoworkerInvoiceHistories.API;

/// <summary>Strongly-typed access to the CoworkerInvoiceHistory endpoints.</summary>
public sealed class CoworkerInvoiceHistoriesApi : NexudusEndpoint<CoworkerInvoiceHistory>
{
    public CoworkerInvoiceHistoriesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerinvoicehistories";

    public Task<PagedResult<CoworkerInvoiceHistory>> SearchCoworkerInvoiceHistories(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerInvoiceHistory?> GetOneCoworkerInvoiceHistory(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerInvoiceHistory>> GetMultipleCoworkerInvoiceHistories(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerInvoiceHistory(CoworkerInvoiceHistory record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerInvoiceHistory(CoworkerInvoiceHistory record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerInvoiceHistory(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerInvoiceHistory> EnumerateCoworkerInvoiceHistories(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
