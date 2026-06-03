using System.Runtime.CompilerServices;

namespace Nexudus;

/// <summary>
/// Read-oriented operations for endpoints whose API does not support create or delete. Exposes search, get,
/// batch-get, and streaming. <see cref="UpdateAsync"/> is provided as a <see langword="protected"/> method:
/// read-only entities that the API allows updating surface it via an entity-named wrapper, while purely
/// read-only entities simply omit that wrapper. Create and delete are intentionally absent so they cannot be
/// called where the API does not support them.
/// </summary>
/// <typeparam name="T">The entity model type.</typeparam>
public abstract class ReadOnlyEndpoint<T> where T : NexudusEntity, new()
{
    protected NexudusClient Client { get; }

    /// <summary>The resource path relative to the API base, without leading or trailing slash.</summary>
    protected abstract string ResourcePath { get; }

    protected ReadOnlyEndpoint(NexudusClient client)
        => Client = client ?? throw new ArgumentNullException(nameof(client));

    /// <summary>Search/list records (one page).</summary>
    public Task<PagedResult<T>> SearchAsync(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => Client.GetPagedAsync<T>(ResourcePath, parameters, cancellationToken);

    /// <summary>Retrieve a single, fully-populated record by Id. Returns null if not found.</summary>
    public Task<T?> GetOneAsync(long id, CancellationToken cancellationToken = default)
        => Client.GetJsonAsync<T>($"{ResourcePath}/{id}", cancellationToken);

    /// <summary>Retrieve several records by Id in one request.</summary>
    public Task<IReadOnlyList<T>> GetManyAsync(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => Client.GetBatchAsync<T>(ResourcePath, ids, cancellationToken);

    /// <summary>
    /// Update a record. Always send a complete record retrieved via <see cref="GetOneAsync"/>:
    /// the API has no PATCH, so omitted fields are cleared. Protected so that concrete endpoints expose it
    /// (via an entity-named wrapper) only when the API supports updating the entity.
    /// </summary>
    protected Task<CommandResult> UpdateAsync(T record, CancellationToken cancellationToken = default)
        => Client.UpdateAsync(ResourcePath, record, cancellationToken);

    /// <summary>
    /// Stream every matching record, transparently following pagination. Handy for exporting or
    /// processing large result sets without managing page numbers yourself.
    /// </summary>
    public async IAsyncEnumerable<T> EnumerateAsync(
        SearchParameters? parameters = null,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var p = parameters ?? new SearchParameters();
        p.Page ??= 1;
        p.Size ??= 100;

        while (true)
        {
            var page = await SearchAsync(p, cancellationToken).ConfigureAwait(false);
            foreach (var record in page.Records)
                yield return record;

            if (!page.HasNextPage || page.Records.Count == 0)
                yield break;

            p.Page = page.CurrentPage + 1;
        }
    }
}
