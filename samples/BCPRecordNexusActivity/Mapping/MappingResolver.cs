using BcpRecordNexusActivity.Configuration;

namespace BcpRecordNexusActivity.Mapping;

/// <summary>
/// Resolves Nexudus values to MRI values using the two configured maps.
/// <list type="bullet">
/// <item>Entity: the first <see cref="EntityMapping"/> whose <c>MRI_Entity_Identifier</c> is contained in the
/// invoice number wins (mapping order = precedence).</item>
/// <item>GL account: 1:1 lookup of a line's <c>FinancialAccountCode</c> to an MRI account (case-insensitive).</item>
/// </list>
/// </summary>
public sealed class MappingResolver
{
    private readonly IReadOnlyList<EntityMapping> _entities;
    private readonly Dictionary<string, string> _glAccounts;

    public MappingResolver(IReadOnlyList<EntityMapping> entityMappings, IReadOnlyList<GlAccountMapping> glAccountMappings)
    {
        _entities = entityMappings;
        _glAccounts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var m in glAccountMappings)
            if (!string.IsNullOrWhiteSpace(m.NexudusAccountCode))
                _glAccounts[m.NexudusAccountCode.Trim()] = m.MriAccountCode;
    }

    /// <summary>First entity mapping whose identifier is contained in <paramref name="invoiceNumber"/>, else null.</summary>
    public EntityMapping? ResolveEntity(string? invoiceNumber)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
            return null;

        foreach (var m in _entities)
            if (!string.IsNullOrEmpty(m.MriEntityIdentifier) &&
                invoiceNumber.Contains(m.MriEntityIdentifier, StringComparison.OrdinalIgnoreCase))
                return m;

        return null;
    }

    /// <summary>MRI GL account for a Nexudus financial account code, or null if unmapped/blank.</summary>
    public string? ResolveGlAccount(string? financialAccountCode)
    {
        if (string.IsNullOrWhiteSpace(financialAccountCode))
            return null;
        return _glAccounts.TryGetValue(financialAccountCode.Trim(), out var mri) ? mri : null;
    }
}
