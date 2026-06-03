namespace Nexudus.Billing.LedgerEntries.Models;

/// <summary>
/// A credit or debit record in the financial ledger for invoices issued by Nexudus to the operator.
/// <para>
/// The API exposes only search and get for ledger entries (no create, update, or delete), so this endpoint
/// derives from <see cref="ReadOnlyEndpoint{T}"/> and exposes no write methods. Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one LedgerEntry" response.
/// </para>
/// </summary>
public sealed class LedgerEntry : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal Balance { get; set; }
}
