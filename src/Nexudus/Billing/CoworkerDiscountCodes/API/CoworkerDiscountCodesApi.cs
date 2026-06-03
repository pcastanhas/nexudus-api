using Nexudus.Billing.CoworkerDiscountCodes.Models;

namespace Nexudus.Billing.CoworkerDiscountCodes.API;

/// <summary>Strongly-typed access to the CoworkerDiscountCode endpoints.</summary>
public sealed class CoworkerDiscountCodesApi : NexudusEndpoint<CoworkerDiscountCode>
{
    public CoworkerDiscountCodesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/coworkerdiscountcodes";

    public Task<PagedResult<CoworkerDiscountCode>> SearchCoworkerDiscountCodes(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<CoworkerDiscountCode?> GetOneCoworkerDiscountCode(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<CoworkerDiscountCode>> GetMultipleCoworkerDiscountCodes(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateCoworkerDiscountCode(CoworkerDiscountCode record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateCoworkerDiscountCode(CoworkerDiscountCode record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteCoworkerDiscountCode(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<CoworkerDiscountCode> EnumerateCoworkerDiscountCodes(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
