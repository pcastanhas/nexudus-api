using Nexudus.Billing.ProposalSchedules.Models;

namespace Nexudus.Billing.ProposalSchedules.API;

/// <summary>Strongly-typed access to the ProposalSchedule endpoints.</summary>
public sealed class ProposalSchedulesApi : NexudusEndpoint<ProposalSchedule>
{
    public ProposalSchedulesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/proposalschedules";

    public Task<PagedResult<ProposalSchedule>> SearchProposalSchedules(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProposalSchedule?> GetOneProposalSchedule(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProposalSchedule>> GetMultipleProposalSchedules(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProposalSchedule(ProposalSchedule record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProposalSchedule(ProposalSchedule record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProposalSchedule(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProposalSchedule> EnumerateProposalSchedules(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
