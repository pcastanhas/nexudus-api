using Nexudus.Billing.CoworkerLedgerEntries.Models;

namespace Nexudus.Billing.CoworkerLedgerEntries.API;

/// <summary>Strongly-typed access to the CoworkerLedgerEntry endpoints (the customer payments ledger).</summary>
public sealed class CoworkerLedgerEntriesApi : NexudusEndpoint<CoworkerLedgerEntry>
{
    public CoworkerLedgerEntriesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerledgerentries";

    /// <summary>Search/list ledger entries (one page) with optional pagination, sorting, and filters.</summary>
    public Task<PagedResult<CoworkerLedgerEntry>> SearchCoworkerLedgerEntries(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    /// <summary>Search/list ledger entries using the strongly-typed <see cref="CoworkerLedgerEntryFilter"/>.</summary>
    public Task<PagedResult<CoworkerLedgerEntry>> SearchCoworkerLedgerEntries(CoworkerLedgerEntryFilter filter, CancellationToken cancellationToken = default)
        => SearchAsync(filter, cancellationToken);

    public Task<CoworkerLedgerEntry?> GetOneCoworkerLedgerEntry(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerLedgerEntry>> GetMultipleCoworkerLedgerEntries(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerLedgerEntry(CoworkerLedgerEntry record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerLedgerEntry(CoworkerLedgerEntry record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerLedgerEntry(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerLedgerEntry> EnumerateCoworkerLedgerEntries(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
