using System.Runtime.CompilerServices;

namespace Nexudus;

/// <summary>
/// Generic CRUD operations shared by every entity endpoint. A concrete endpoint only needs to
/// supply its <see cref="ResourcePath"/> (e.g. <c>"billing/charges"</c>) and expose
/// entity-named wrappers around these methods.
/// </summary>
/// <typeparam name="T">The entity model type.</typeparam>
public abstract class NexudusEndpoint<T> where T : NexudusEntity, new()
{
    protected NexudusClient Client { get; }

    /// <summary>The resource path relative to the API base, without leading or trailing slash.</summary>
    protected abstract string ResourcePath { get; }

    protected NexudusEndpoint(NexudusClient client)
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

    /// <summary>Create a record. Returns the new record's Id.</summary>
    public Task<long> CreateAsync(T record, CancellationToken cancellationToken = default)
        => Client.CreateAsync(ResourcePath, record, cancellationToken);

    /// <summary>
    /// Update a record. Always send a complete record retrieved via <see cref="GetOneAsync"/>:
    /// the API has no PATCH, so omitted fields are cleared.
    /// </summary>
    public Task<CommandResult> UpdateAsync(T record, CancellationToken cancellationToken = default)
        => Client.UpdateAsync(ResourcePath, record, cancellationToken);

    /// <summary>Delete a record by Id.</summary>
    public Task<CommandResult> DeleteAsync(long id, CancellationToken cancellationToken = default)
        => Client.DeleteAsync($"{ResourcePath}/{id}", cancellationToken);

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
