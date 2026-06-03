using Nexudus.Billing.LedgerEntries.Models;

namespace Nexudus.Billing.LedgerEntries.API;

/// <summary>
/// Strongly-typed access to the LedgerEntry endpoints. The API exposes only search and get for ledger
/// entries, so this is a read-only endpoint with no create, update, or delete methods.
/// </summary>
public sealed class LedgerEntriesApi : ReadOnlyEndpoint<LedgerEntry>
{
    public LedgerEntriesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/ledgerentries";

    public Task<PagedResult<LedgerEntry>> SearchLedgerEntries(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<LedgerEntry?> GetOneLedgerEntry(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<LedgerEntry>> GetMultipleLedgerEntries(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public IAsyncEnumerable<LedgerEntry> EnumerateLedgerEntries(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
