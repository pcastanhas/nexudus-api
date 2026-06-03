using System.Text.Json;

namespace Nexudus;

/// <summary>
/// Fields common to every Nexudus record. Entity models (e.g. <c>Charge</c>) inherit from this.
/// </summary>
public abstract class NexudusEntity
{
    /// <summary>Numeric primary key.</summary>
    public long Id { get; set; }

    /// <summary>GUID identifier, used by cross-entity references.</summary>
    public string? UniqueId { get; set; }

    public DateTimeOffset? CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }

    /// <summary>Email of the user who last updated the record.</summary>
    public string? UpdatedBy { get; set; }

    /// <summary>True when the record was just created.</summary>
    public bool IsNew { get; set; }

    /// <summary>External system identifier, if any.</summary>
    public string? SystemId { get; set; }

    /// <summary>Human-readable label the API uses for the record.</summary>
    public string? ToStringText { get; set; }

    /// <summary>Raw localization payload (shape varies by entity).</summary>
    public JsonElement? LocalizationDetails { get; set; }

    /// <summary>Raw custom-fields payload (shape varies by account configuration).</summary>
    public JsonElement? CustomFields { get; set; }
}
