namespace Nexudus;

/// <summary>
/// The paged envelope returned by every list/search endpoint.
/// </summary>
/// <typeparam name="T">The entity type contained in <see cref="Records"/>.</typeparam>
public sealed class PagedResult<T>
{
    /// <summary>
    /// The records on the current page. Note: list endpoints return a summary
    /// representation; some fields are only populated by the "get one" endpoint.
    /// </summary>
    public List<T> Records { get; set; } = new();

    public int CurrentPage { get; set; }
    public int CurrentPageSize { get; set; }
    public string? CurrentOrderField { get; set; }
    public int CurrentSortDirection { get; set; }
    public int FirstItem { get; set; }
    public int LastItem { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}
