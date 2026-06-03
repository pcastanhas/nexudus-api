using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nexudus.Internal;

namespace Nexudus;

/// <summary>
/// The connection to a Nexudus account: holds the <see cref="HttpClient"/>, manages the bearer
/// token (acquire + refresh), and performs the underlying HTTP calls. Construct it once and reuse
/// it; pass it to endpoint classes such as <c>ChargesApi</c>.
/// </summary>
public sealed class NexudusClient : IDisposable
{
    private readonly NexudusClientOptions _options;
    private readonly HttpClient _http;
    private readonly bool _ownsHttp;
    private readonly SemaphoreSlim _authLock = new(1, 1);
    private readonly bool _staticToken;

    private string? _accessToken;
    private string? _refreshToken;
    private DateTimeOffset _expiresAtUtc = DateTimeOffset.MinValue;

    // PascalCase property names match the API exactly, so no naming policy is used.
    internal static readonly JsonSerializerOptions ReadOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // Create: omit null fields so the server applies its own defaults.
    internal static readonly JsonSerializerOptions CreateOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Update: send the complete record (the API clears omitted fields).
    internal static readonly JsonSerializerOptions UpdateOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public NexudusClient(NexudusClientOptions options, HttpClient? httpClient = null)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
            throw new ArgumentException("BaseUrl is required.", nameof(options));

        _http = httpClient ?? new HttpClient();
        _ownsHttp = httpClient is null;

        _accessToken = options.AccessToken;
        _refreshToken = options.RefreshToken;

        // A caller-supplied token with no credentials is treated as static (never refreshed).
        _staticToken = _accessToken is not null
                       && string.IsNullOrEmpty(options.Username)
                       && string.IsNullOrEmpty(options.RefreshToken);
        if (_accessToken is not null)
            _expiresAtUtc = DateTimeOffset.MaxValue;
    }

    /// <summary>Convenience factory for username/password authentication.</summary>
    public static NexudusClient WithPassword(string username, string password, string? totp = null, string? baseUrl = null)
        => new(new NexudusClientOptions
        {
            Username = username,
            Password = password,
            Totp = totp,
            BaseUrl = baseUrl ?? new NexudusClientOptions().BaseUrl
        });

    /// <summary>Convenience factory for a pre-acquired bearer token.</summary>
    public static NexudusClient WithToken(string accessToken, string? baseUrl = null)
        => new(new NexudusClientOptions
        {
            AccessToken = accessToken,
            BaseUrl = baseUrl ?? new NexudusClientOptions().BaseUrl
        });

    // ---- URI building -------------------------------------------------------

    private Uri BuildUri(string path, IReadOnlyDictionary<string, string>? query = null)
    {
        var sb = new StringBuilder();
        sb.Append(_options.BaseUrl.TrimEnd('/')).Append('/').Append(path.TrimStart('/'));
        if (query is { Count: > 0 })
        {
            sb.Append('?');
            var first = true;
            foreach (var kv in query)
            {
                if (!first) sb.Append('&');
                first = false;
                sb.Append(Uri.EscapeDataString(kv.Key)).Append('=').Append(Uri.EscapeDataString(kv.Value));
            }
        }
        return new Uri(sb.ToString());
    }

    // ---- Authentication -----------------------------------------------------

    /// <summary>Force a fresh password-grant authentication now.</summary>
    public async Task AuthenticateAsync(CancellationToken cancellationToken = default)
    {
        await _authLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try { await RequestTokenAsync(refresh: false, cancellationToken).ConfigureAwait(false); }
        finally { _authLock.Release(); }
    }

    private async Task EnsureTokenAsync(CancellationToken cancellationToken)
    {
        if (_staticToken)
            return;

        if (_accessToken is not null && DateTimeOffset.UtcNow < _expiresAtUtc - _options.RefreshSkew)
            return;

        await _authLock.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_accessToken is not null && DateTimeOffset.UtcNow < _expiresAtUtc - _options.RefreshSkew)
                return;

            if (_refreshToken is not null &&
                await RequestTokenAsync(refresh: true, cancellationToken).ConfigureAwait(false))
                return;

            await RequestTokenAsync(refresh: false, cancellationToken).ConfigureAwait(false);
        }
        finally { _authLock.Release(); }
    }

    private async Task<bool> RequestTokenAsync(bool refresh, CancellationToken cancellationToken)
    {
        var form = new List<KeyValuePair<string, string>>();
        if (refresh && _refreshToken is not null)
        {
            form.Add(new("grant_type", "refresh_token"));
            form.Add(new("refresh_token", _refreshToken));
        }
        else
        {
            if (string.IsNullOrEmpty(_options.Username) || string.IsNullOrEmpty(_options.Password))
                throw new NexudusApiException(
                    "No credentials configured. Set Username/Password or supply a valid AccessToken.");
            form.Add(new("grant_type", "password"));
            form.Add(new("username", _options.Username!));
            form.Add(new("password", _options.Password!));
            if (!string.IsNullOrEmpty(_options.Totp))
                form.Add(new("totp", _options.Totp!));
        }

        using var request = new HttpRequestMessage(HttpMethod.Post, BuildUri("token"))
        {
            Content = new FormUrlEncodedContent(form)
        };
        using var response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            if (refresh) { _refreshToken = null; return false; } // fall back to password grant
            throw new NexudusApiException(
                $"Authentication failed ({(int)response.StatusCode}).", response.StatusCode, null, body);
        }

        var token = JsonSerializer.Deserialize<TokenResponse>(body, ReadOptions);
        if (token?.AccessToken is null)
            throw new NexudusApiException(
                "Authentication response did not contain an access_token.", response.StatusCode, null, body);

        _accessToken = token.AccessToken;
        if (token.RefreshToken is not null) _refreshToken = token.RefreshToken;
        _expiresAtUtc = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn <= 0 ? 3600 : token.ExpiresIn);
        return true;
    }

    // ---- Core send (with one auth retry) ------------------------------------

    private async Task<HttpResponseMessage> SendAsync(
        HttpMethod method, Uri uri, Func<HttpContent?>? contentFactory, CancellationToken cancellationToken)
    {
        for (var attempt = 0; ; attempt++)
        {
            await EnsureTokenAsync(cancellationToken).ConfigureAwait(false);

            var request = new HttpRequestMessage(method, uri);
            if (_accessToken is not null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Content = contentFactory?.Invoke();

            var response = await _http.SendAsync(request, cancellationToken).ConfigureAwait(false);
            request.Dispose();

            if (response.StatusCode == HttpStatusCode.Unauthorized && attempt == 0 && !_staticToken)
            {
                response.Dispose();
                _expiresAtUtc = DateTimeOffset.MinValue; // force re-auth on next loop
                continue;
            }
            return response;
        }
    }

    // ---- Typed helpers used by NexudusEndpoint<T> ---------------------------

    internal async Task<T?> GetJsonAsync<T>(string path, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(HttpMethod.Get, BuildUri(path), null, cancellationToken).ConfigureAwait(false);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return default;
        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        EnsureSuccess(response, body);
        return JsonSerializer.Deserialize<T>(body, ReadOptions);
    }

    internal async Task<PagedResult<T>> GetPagedAsync<T>(string path, SearchParameters? parameters, CancellationToken cancellationToken)
    {
        var query = (parameters ?? new SearchParameters()).Build();
        using var response = await SendAsync(HttpMethod.Get, BuildUri(path, query), null, cancellationToken).ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        EnsureSuccess(response, body);
        return JsonSerializer.Deserialize<PagedResult<T>>(body, ReadOptions) ?? new PagedResult<T>();
    }

    internal async Task<IReadOnlyList<T>> GetBatchAsync<T>(string path, IEnumerable<long> ids, CancellationToken cancellationToken)
    {
        var idList = string.Join(",", ids);
        // The batch endpoint expects: {path}/?id=[1,2,3]
        var uri = new Uri($"{_options.BaseUrl.TrimEnd('/')}/{path.TrimStart('/')}/?id=[{idList}]");
        using var response = await SendAsync(HttpMethod.Get, uri, null, cancellationToken).ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        EnsureSuccess(response, body);
        return JsonSerializer.Deserialize<List<T>>(body, ReadOptions) ?? new List<T>();
    }

    internal async Task<long> CreateAsync<T>(string path, T record, CancellationToken cancellationToken)
    {
        var result = await SendCommandAsync(
            HttpMethod.Post, BuildUri(path),
            () => JsonContent.Create(record, options: CreateOptions),
            cancellationToken).ConfigureAwait(false);
        return result.CreatedId ?? 0;
    }

    internal Task<CommandResult> UpdateAsync<T>(string path, T record, CancellationToken cancellationToken)
        => SendCommandAsync(
            HttpMethod.Put, BuildUri(path),
            () => JsonContent.Create(record, options: UpdateOptions),
            cancellationToken);

    internal Task<CommandResult> DeleteAsync(string path, CancellationToken cancellationToken)
        => SendCommandAsync(HttpMethod.Delete, BuildUri(path), null, cancellationToken);

    private async Task<CommandResult> SendCommandAsync(
        HttpMethod method, Uri uri, Func<HttpContent?>? contentFactory, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(method, uri, contentFactory, cancellationToken).ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        CommandResult? result = null;
        if (!string.IsNullOrWhiteSpace(body))
        {
            try { result = JsonSerializer.Deserialize<CommandResult>(body, ReadOptions); }
            catch (JsonException) { /* non-JSON body; handled below */ }
        }

        if (!response.IsSuccessStatusCode || result is { WasSuccessful: false })
        {
            throw new NexudusApiException(
                result?.Message ?? $"Request failed with status {(int)response.StatusCode}.",
                response.StatusCode,
                result?.Errors,
                body);
        }

        return result ?? new CommandResult { WasSuccessful = true, Status = (int)response.StatusCode };
    }

    private static void EnsureSuccess(HttpResponseMessage response, string body)
    {
        if (response.IsSuccessStatusCode)
            return;

        List<ValidationError>? errors = null;
        string? message = null;
        try
        {
            var cr = JsonSerializer.Deserialize<CommandResult>(body, ReadOptions);
            errors = cr?.Errors;
            message = cr?.Message;
        }
        catch (JsonException) { /* leave message null */ }

        throw new NexudusApiException(
            message ?? $"Request failed with status {(int)response.StatusCode}.",
            response.StatusCode, errors, body);
    }

    public void Dispose()
    {
        if (_ownsHttp) _http.Dispose();
        _authLock.Dispose();
    }
}
