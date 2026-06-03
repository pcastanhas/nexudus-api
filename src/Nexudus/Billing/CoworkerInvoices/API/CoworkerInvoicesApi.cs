using Nexudus.Billing.CoworkerInvoices.Models;

namespace Nexudus.Billing.CoworkerInvoices.API;

/// <summary>
/// Strongly-typed access to the CoworkerInvoice endpoints. The API supports search, get, and update for
/// customer invoices but not create or delete, so no create/delete methods are exposed.
/// </summary>
public sealed class CoworkerInvoicesApi : ReadOnlyEndpoint<CoworkerInvoice>
{
    public CoworkerInvoicesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerinvoices";

    public Task<PagedResult<CoworkerInvoice>> SearchCoworkerInvoices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerInvoice?> GetOneCoworkerInvoice(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerInvoice>> GetMultipleCoworkerInvoices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    /// <summary>
    /// Update a customer invoice (e.g. while it is still a draft). Pass a complete record fetched via
    /// <see cref="GetOneCoworkerInvoice"/>; the API has no PATCH, so omitted fields are cleared.
    /// (Customer invoices cannot be created or deleted via the API.)
    /// </summary>
    public Task<CommandResult> UpdateCoworkerInvoice(CoworkerInvoice invoice, CancellationToken cancellationToken = default)
        => UpdateAsync(invoice, cancellationToken);

    public IAsyncEnumerable<CoworkerInvoice> EnumerateCoworkerInvoices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
