using Nexudus.Billing.CoworkerInvoicePaymentTokens.Models;

namespace Nexudus.Billing.CoworkerInvoicePaymentTokens.API;

/// <summary>Strongly-typed access to the CoworkerInvoicePaymentToken endpoints.</summary>
public sealed class CoworkerInvoicePaymentTokensApi : NexudusEndpoint<CoworkerInvoicePaymentToken>
{
    public CoworkerInvoicePaymentTokensApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerinvoicepaymenttokens";

    public Task<PagedResult<CoworkerInvoicePaymentToken>> SearchCoworkerInvoicePaymentTokens(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerInvoicePaymentToken?> GetOneCoworkerInvoicePaymentToken(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerInvoicePaymentToken>> GetMultipleCoworkerInvoicePaymentTokens(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerInvoicePaymentToken(CoworkerInvoicePaymentToken record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerInvoicePaymentToken(CoworkerInvoicePaymentToken record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerInvoicePaymentToken(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerInvoicePaymentToken> EnumerateCoworkerInvoicePaymentTokens(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
