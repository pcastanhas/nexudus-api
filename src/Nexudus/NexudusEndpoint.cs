namespace Nexudus;

/// <summary>
/// Full CRUD operations shared by every entity endpoint whose API supports create and delete. Inherits the
/// read operations (and the protected <see cref="ReadOnlyEndpoint{T}.UpdateAsync"/>) from
/// <see cref="ReadOnlyEndpoint{T}"/> and adds create and delete. A concrete endpoint only needs to supply
/// its <see cref="ReadOnlyEndpoint{T}.ResourcePath"/> and expose entity-named wrappers around these methods.
/// </summary>
/// <typeparam name="T">The entity model type.</typeparam>
public abstract class NexudusEndpoint<T> : ReadOnlyEndpoint<T> where T : NexudusEntity, new()
{
    protected NexudusEndpoint(NexudusClient client) : base(client) { }

    /// <summary>Create a record. Returns the new record's Id.</summary>
    public Task<long> CreateAsync(T record, CancellationToken cancellationToken = default)
        => Client.CreateAsync(ResourcePath, record, cancellationToken);

    /// <summary>Delete a record by Id.</summary>
    public Task<CommandResult> DeleteAsync(long id, CancellationToken cancellationToken = default)
        => Client.DeleteAsync($"{ResourcePath}/{id}", cancellationToken);
}
