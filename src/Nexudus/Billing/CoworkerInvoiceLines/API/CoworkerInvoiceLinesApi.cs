using Nexudus.Billing.CoworkerInvoiceLines.Models;

namespace Nexudus.Billing.CoworkerInvoiceLines.API;

/// <summary>
/// Strongly-typed access to the CoworkerInvoiceLine endpoints. The API supports search, get, and update for
/// invoice lines but not create or delete, so no create/delete methods are exposed.
/// </summary>
public sealed class CoworkerInvoiceLinesApi : ReadOnlyEndpoint<CoworkerInvoiceLine>
{
    public CoworkerInvoiceLinesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerinvoicelines";

    public Task<PagedResult<CoworkerInvoiceLine>> SearchCoworkerInvoiceLines(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerInvoiceLine?> GetOneCoworkerInvoiceLine(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerInvoiceLine>> GetMultipleCoworkerInvoiceLines(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    /// <summary>
    /// Update an invoice line. Pass a complete record fetched via <see cref="GetOneCoworkerInvoiceLine"/>;
    /// the API has no PATCH, so omitted fields are cleared. (Invoice lines cannot be created or deleted via
    /// the API.)
    /// </summary>
    public Task<CommandResult> UpdateCoworkerInvoiceLine(CoworkerInvoiceLine line, CancellationToken cancellationToken = default)
        => UpdateAsync(line, cancellationToken);

    public IAsyncEnumerable<CoworkerInvoiceLine> EnumerateCoworkerInvoiceLines(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
