using Nexudus.Billing.TimePassTimeSlots.Models;

namespace Nexudus.Billing.TimePassTimeSlots.API;

/// <summary>Strongly-typed access to the TimePassTimeSlot endpoints.</summary>
public sealed class TimePassTimeSlotsApi : NexudusEndpoint<TimePassTimeSlot>
{
    public TimePassTimeSlotsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/timepasstimeslots";

    public Task<PagedResult<TimePassTimeSlot>> SearchTimePassTimeSlots(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<TimePassTimeSlot?> GetOneTimePassTimeSlot(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<TimePassTimeSlot>> GetMultipleTimePassTimeSlots(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateTimePassTimeSlot(TimePassTimeSlot record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateTimePassTimeSlot(TimePassTimeSlot record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteTimePassTimeSlot(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<TimePassTimeSlot> EnumerateTimePassTimeSlots(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
