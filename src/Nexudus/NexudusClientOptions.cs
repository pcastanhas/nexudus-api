namespace Nexudus;

/// <summary>
/// Configuration for <see cref="NexudusClient"/>.
/// Supply either a username/password pair (the client will obtain and refresh a
/// bearer token automatically) or a pre-acquired <see cref="AccessToken"/>.
/// </summary>
public sealed class NexudusClientOptions
{
    /// <summary>Root of the REST API. Defaults to the Nexudus production host.</summary>
    public string BaseUrl { get; set; } = "https://spaces.nexudus.com/api";

    /// <summary>Account email used for the password grant.</summary>
    public string? Username { get; set; }

    /// <summary>Account password used for the password grant.</summary>
    public string? Password { get; set; }

    /// <summary>Current TOTP code, required only when the account has 2FA enabled.</summary>
    public string? Totp { get; set; }

    /// <summary>
    /// A pre-acquired bearer token. When set (and no username/password is given) the
    /// client uses it as-is and does not attempt to refresh it.
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>Optional refresh token to renew an expired access token without re-sending credentials.</summary>
    public string? RefreshToken { get; set; }

    /// <summary>How early (before expiry) a token is proactively refreshed.</summary>
    public TimeSpan RefreshSkew { get; set; } = TimeSpan.FromMinutes(2);
}
