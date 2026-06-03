using Nexudus.Billing.Invoices.Models;

namespace Nexudus.Billing.Invoices.API;

/// <summary>
/// Strongly-typed access to the Invoice endpoints. The API supports search, get, and update for invoices but
/// not create or delete, so no create/delete methods are exposed.
/// </summary>
public sealed class InvoicesApi : ReadOnlyEndpoint<Invoice>
{
    public InvoicesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/invoices";

    public Task<PagedResult<Invoice>> SearchInvoices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<Invoice?> GetOneInvoice(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<Invoice>> GetMultipleInvoices(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    /// <summary>
    /// Update an invoice. Pass a complete record fetched via <see cref="GetOneInvoice"/>; the API has no
    /// PATCH, so omitted fields are cleared. (Invoices cannot be created or deleted via the API.)
    /// </summary>
    public Task<CommandResult> UpdateInvoice(Invoice invoice, CancellationToken cancellationToken = default)
        => UpdateAsync(invoice, cancellationToken);

    public IAsyncEnumerable<Invoice> EnumerateInvoices(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
