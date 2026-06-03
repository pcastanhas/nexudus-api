using Nexudus.Billing.ProposalContractSchedules.Models;

namespace Nexudus.Billing.ProposalContractSchedules.API;

/// <summary>Strongly-typed access to the ProposalContractSchedule endpoints.</summary>
public sealed class ProposalContractSchedulesApi : NexudusEndpoint<ProposalContractSchedule>
{
    public ProposalContractSchedulesApi(NexudusClient client) : base(client) { }

    protected override string ResourcePath => "billing/proposalcontractschedules";

    public Task<PagedResult<ProposalContractSchedule>> SearchProposalContractSchedules(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => SearchAsync(parameters, cancellationToken);

    public Task<ProposalContractSchedule?> GetOneProposalContractSchedule(long id, CancellationToken cancellationToken = default)
        => GetOneAsync(id, cancellationToken);

    public Task<IReadOnlyList<ProposalContractSchedule>> GetMultipleProposalContractSchedules(IEnumerable<long> ids, CancellationToken cancellationToken = default)
        => GetManyAsync(ids, cancellationToken);

    public Task<long> CreateProposalContractSchedule(ProposalContractSchedule record, CancellationToken cancellationToken = default)
        => CreateAsync(record, cancellationToken);

    public Task<CommandResult> UpdateProposalContractSchedule(ProposalContractSchedule record, CancellationToken cancellationToken = default)
        => UpdateAsync(record, cancellationToken);

    public Task<CommandResult> DeleteProposalContractSchedule(long id, CancellationToken cancellationToken = default)
        => DeleteAsync(id, cancellationToken);

    public IAsyncEnumerable<ProposalContractSchedule> EnumerateProposalContractSchedules(SearchParameters? parameters = null, CancellationToken cancellationToken = default)
        => EnumerateAsync(parameters, cancellationToken);
}
