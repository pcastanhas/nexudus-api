namespace Nexudus.Billing.CoworkerLedgerEntries.API;

/// <summary>
/// Strongly-typed, fluent filters for <c>SearchCoworkerLedgerEntries</c>. These map to the API's documented
/// <c>CoworkerLedgerEntry_*</c> / <c>from_</c> / <c>to_</c> query parameters. A representative subset is
/// provided; any other documented filter can be added with the inherited
/// <see cref="SearchParameters.Where(string, string)"/>.
/// <para>
/// In a customer ledger, payments are recorded as credit entries. <see cref="PaymentsBetween"/> selects the
/// entries whose transaction date falls in the given range (combine with <see cref="ForCoworker"/> for a
/// single customer's payments over a period).
/// </para>
/// </summary>
public sealed class CoworkerLedgerEntryFilter : SearchParameters
{
    public CoworkerLedgerEntryFilter ForCoworker(long coworkerId) { Where("CoworkerLedgerEntry_Coworker", coworkerId); return this; }
    public CoworkerLedgerEntryFilter ForBusiness(long businessId) { Where("CoworkerLedgerEntry_Business", businessId); return this; }
    public CoworkerLedgerEntryFilter ForInvoice(long coworkerInvoiceId) { Where("CoworkerLedgerEntry_CoworkerInvoice", coworkerInvoiceId); return this; }
    public CoworkerLedgerEntryFilter IsBilled(bool billed = true) { Where("CoworkerLedgerEntry_Billed", billed); return this; }

    /// <summary>Filters to ledger entries whose transaction date falls within the given range.</summary>
    public CoworkerLedgerEntryFilter TransactionDateBetween(DateTimeOffset from, DateTimeOffset to)
    {
        Where("from_CoworkerLedgerEntry_TransactionDate", from);
        Where("to_CoworkerLedgerEntry_TransactionDate", to);
        return this;
    }

    /// <summary>
    /// Selects payments made in the given date range. Payments are credit entries in the ledger, so this is
    /// an alias of <see cref="TransactionDateBetween"/> scoped to the transaction date.
    /// </summary>
    public CoworkerLedgerEntryFilter PaymentsBetween(DateTimeOffset from, DateTimeOffset to)
        => TransactionDateBetween(from, to);

    public CoworkerLedgerEntryFilter CreatedBetween(DateTimeOffset from, DateTimeOffset to)
    {
        Where("from_CoworkerLedgerEntry_CreatedOn", from);
        Where("to_CoworkerLedgerEntry_CreatedOn", to);
        return this;
    }

    public CoworkerLedgerEntryFilter UpdatedBetween(DateTimeOffset from, DateTimeOffset to)
    {
        Where("from_CoworkerLedgerEntry_UpdatedOn", from);
        Where("to_CoworkerLedgerEntry_UpdatedOn", to);
        return this;
    }
}
