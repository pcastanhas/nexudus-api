using System.Text.Json;

namespace Nexudus;

/// <summary>A single field-level validation error returned by create/update requests.</summary>
public sealed class ValidationError
{
    public string? PropertyName { get; set; }
    public string? Message { get; set; }
    public JsonElement? AttemptedValue { get; set; }

    public override string ToString() => $"{PropertyName}: {Message}";
}
