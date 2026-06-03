using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nexudus;

/// <summary>
/// The envelope returned by create / update / delete (and other "command") endpoints.
/// </summary>
public sealed class CommandResult
{
    public int? Status { get; set; }
    public string? Message { get; set; }
    public bool WasSuccessful { get; set; }
    public List<ValidationError>? Errors { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public bool OpenInDialog { get; set; }
    public bool OpenInWindow { get; set; }
    public string? RedirectURL { get; set; }
    public string? JavaScript { get; set; }

    /// <summary>Raw <c>Value</c> payload. For create requests this contains the new record's <c>Id</c>.</summary>
    public JsonElement? Value { get; set; }

    /// <summary>Convenience accessor for the new record Id returned by a create request.</summary>
    [JsonIgnore]
    public long? CreatedId
    {
        get
        {
            if (Value is { ValueKind: JsonValueKind.Object } v &&
                v.TryGetProperty("Id", out var idProp) &&
                idProp.TryGetInt64(out var id))
            {
                return id;
            }
            return null;
        }
    }
}
