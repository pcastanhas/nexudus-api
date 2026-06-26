using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BcpRecordNexusActivity.Configuration;

/// <summary>
/// Strongly-typed view of <c>appsettings.json</c>. Loaded with <see cref="AppSettingsLoader"/>.
/// </summary>
public sealed class AppSettings
{
    public RunSettings Run { get; init; } = new();
    public JournalSettings Journal { get; init; } = new();
    public NexudusSettings Nexudus { get; init; } = new();
    public SmtpSettings Smtp { get; init; } = new();
    public NotificationSettings Notifications { get; init; } = new();
    public List<EntityMapping> EntityMappings { get; init; } = new();
    public List<GlAccountMapping> GlAccountMappings { get; init; } = new();
}

public sealed class RunSettings
{
    /// <summary>Inclusive start of the date range (invoices by CreatedOn, payments by TransactionDate).</summary>
    public DateTimeOffset FromDate { get; init; }

    /// <summary>Inclusive end of the date range.</summary>
    public DateTimeOffset ToDate { get; init; }

    /// <summary>MRI accounting period, format <c>yyyyMM</c> (e.g. <c>202605</c>). Drives JOURNAL.PERIOD and REF.</summary>
    public string PostingPeriod { get; init; } = "";
}

public sealed class JournalSettings
{
    public string ConnectionString { get; init; } = "";
}

public sealed class NexudusSettings
{
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? Totp { get; init; }
    public string? BaseUrl { get; init; }
}

public sealed class SmtpSettings
{
    public string Host { get; init; } = "";
    public int Port { get; init; } = 587;
    public bool EnableSsl { get; init; } = true;
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string FromAddress { get; init; } = "";
    public string? FromName { get; init; }
}

public sealed class NotificationSettings
{
    public List<string> Recipients { get; init; } = new();
}

/// <summary>One row of the entity mapping. The Nexudus invoice number is matched by <c>Contains</c> on
/// <see cref="MriEntityIdentifier"/>; the first match wins.</summary>
public sealed class EntityMapping
{
    [JsonPropertyName("MRI_Entity_Identifier")] public string MriEntityIdentifier { get; init; } = "";
    [JsonPropertyName("MRI_EntityID")] public string MriEntityId { get; init; } = "";
    [JsonPropertyName("MRI_AR_Account")] public string MriArAccount { get; init; } = "";
    [JsonPropertyName("MRI_Cash_Account")] public string MriCashAccount { get; init; } = "";
}

/// <summary>One row of the 1:1 GL account map: Nexudus FinancialAccountCode to MRI GL account.</summary>
public sealed class GlAccountMapping
{
    [JsonPropertyName("Nexudus_Account_Code")] public string NexudusAccountCode { get; init; } = "";
    [JsonPropertyName("MRI_Account_Code")] public string MriAccountCode { get; init; } = "";
}

public static class AppSettingsLoader
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    /// <summary>Reads and validates the settings file. Throws <see cref="InvalidOperationException"/> on any
    /// problem so the caller can report a clear failure (and email the notification group).</summary>
    public static AppSettings Load(string path)
    {
        if (!File.Exists(path))
            throw new InvalidOperationException($"Configuration file not found: {path}");

        AppSettings settings;
        try
        {
            settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(path), Options)
                       ?? throw new InvalidOperationException("Configuration file deserialized to null.");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Configuration file is not valid JSON: {ex.Message}", ex);
        }

        Validate(settings);
        return settings;
    }

    private static void Validate(AppSettings s)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(s.Journal.ConnectionString))
            errors.Add("Journal.ConnectionString is required.");

        if (!IsValidPostingPeriod(s.Run.PostingPeriod))
            errors.Add($"Run.PostingPeriod must be in yyyyMM format (e.g. 202605); got '{s.Run.PostingPeriod}'.");

        if (s.Run.FromDate == default || s.Run.ToDate == default)
            errors.Add("Run.FromDate and Run.ToDate are required (yyyy-MM-dd).");
        else if (s.Run.FromDate > s.Run.ToDate)
            errors.Add("Run.FromDate must not be after Run.ToDate.");

        if (s.Notifications.Recipients.Count == 0)
            errors.Add("Notifications.Recipients must contain at least one address.");

        if (string.IsNullOrWhiteSpace(s.Smtp.Host) || string.IsNullOrWhiteSpace(s.Smtp.FromAddress))
            errors.Add("Smtp.Host and Smtp.FromAddress are required.");

        if (s.EntityMappings.Count == 0)
            errors.Add("EntityMappings must contain at least one row.");
        if (s.GlAccountMappings.Count == 0)
            errors.Add("GlAccountMappings must contain at least one row.");

        if (errors.Count > 0)
            throw new InvalidOperationException("Invalid configuration:" + Environment.NewLine
                + string.Join(Environment.NewLine, errors.Select(e => "  - " + e)));
    }

    /// <summary>True for a 6-digit yyyyMM string with a 01-12 month.</summary>
    public static bool IsValidPostingPeriod(string? period)
    {
        if (period is null || period.Length != 6 || !period.All(char.IsDigit))
            return false;
        var month = int.Parse(period.Substring(4, 2), CultureInfo.InvariantCulture);
        return month is >= 1 and <= 12;
    }
}
