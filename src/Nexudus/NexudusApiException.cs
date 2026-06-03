using System.Net;

namespace Nexudus;

/// <summary>Thrown when the Nexudus API returns an error status or an unsuccessful command result.</summary>
public sealed class NexudusApiException : Exception
{
    public HttpStatusCode? StatusCode { get; }
    public IReadOnlyList<ValidationError> Errors { get; }
    public string? ResponseBody { get; }

    public NexudusApiException(
        string message,
        HttpStatusCode? statusCode = null,
        IReadOnlyList<ValidationError>? errors = null,
        string? responseBody = null)
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors ?? Array.Empty<ValidationError>();
        ResponseBody = responseBody;
    }
}
