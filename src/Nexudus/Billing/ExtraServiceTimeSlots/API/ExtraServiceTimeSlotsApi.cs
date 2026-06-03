using Nexudus.Billing.ExtraServiceTimeSlots.Models;

namespace Nexudus.Billing.ExtraServiceTimeSlots.API;

/// <summary>Strongly-typed access to the ExtraServiceTimeSlot endpoints.</summary>
public sealed class ExtraServiceTimeSlotsApi : NexudusEndpoint<ExtraServiceTimeSlot>
{
    public ExtraServiceTimeSlotsApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/extraservicetimeslots";

    public Task<PagedResult<ExtraServiceTimeSlot>> SearchExtraServiceTimeSlots(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ExtraServiceTimeSlot?> GetOneExtraServiceTimeSlot(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ExtraServiceTimeSlot>> GetMultipleExtraServiceTimeSlots(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateExtraServiceTimeSlot(ExtraServiceTimeSlot record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateExtraServiceTimeSlot(ExtraServiceTimeSlot record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteExtraServiceTimeSlot(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ExtraServiceTimeSlot> EnumerateExtraServiceTimeSlots(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
