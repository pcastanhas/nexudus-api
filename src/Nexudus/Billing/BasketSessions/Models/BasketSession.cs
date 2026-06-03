namespace Nexudus.Billing.BasketSessions.Models;

/// <summary>
/// An internal entity that temporarily stores basket items while a customer checks out on the Members
/// Portal. Per the API docs it is not intended for direct use; CRUD is exposed for completeness.
/// <para>
/// Fields below the <see cref="NexudusEntity"/> base mirror the "Get one BasketSession" response.
/// </para>
/// </summary>
public sealed class BasketSession : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? SessionId { get; set; }
    public string? JsonContents { get; set; }
}
