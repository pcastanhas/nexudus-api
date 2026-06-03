using System.Globalization;

namespace Nexudus;

/// <summary>
/// Pagination, sorting, and filtering options for a list/search request.
/// <para>
/// Pagination and sorting are strongly typed. Entity-specific filters use the API's
/// <c>Entity_Field</c> naming convention; add them with <see cref="Where(string, string)"/>
/// (and overloads), or use a strongly-typed subclass such as <c>ChargeFilter</c>.
/// </para>
/// </summary>
public class SearchParameters
{
    public int? Page { get; set; }
    public int? Size { get; set; }
    public string? OrderBy { get; set; }
    public SortDirection? Direction { get; set; }

    /// <summary>Raw filter map keyed by API parameter name (e.g. <c>Charge_Invoiced</c>).</summary>
    public Dictionary<string, string> Filters { get; } = new(StringComparer.Ordinal);

    public SearchParameters Paged(int page, int size)
    {
        Page = page;
        Size = size;
        return this;
    }

    public SearchParameters SortBy(string field, SortDirection direction = SortDirection.Ascending)
    {
        OrderBy = field;
        Direction = direction;
        return this;
    }

    public SearchParameters Where(string field, string value)
    {
        Filters[field] = value;
        return this;
    }

    public SearchParameters Where(string field, long value) =>
        Where(field, value.ToString(CultureInfo.InvariantCulture));

    public SearchParameters Where(string field, bool value) =>
        Where(field, value ? "true" : "false");

    public SearchParameters Where(string field, decimal value) =>
        Where(field, value.ToString(CultureInfo.InvariantCulture));

    public SearchParameters Where(string field, DateTimeOffset value) =>
        Where(field, value.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture));

    /// <summary>Materializes the full query string map, including pagination/sort keys.</summary>
    internal IReadOnlyDictionary<string, string> Build()
    {
        var query = new Dictionary<string, string>(Filters, StringComparer.Ordinal);
        if (Page is int page) query["page"] = page.ToString(CultureInfo.InvariantCulture);
        if (Size is int size) query["size"] = size.ToString(CultureInfo.InvariantCulture);
        if (!string.IsNullOrEmpty(OrderBy)) query["orderBy"] = OrderBy!;
        if (Direction is SortDirection dir) query["dir"] = ((int)dir).ToString(CultureInfo.InvariantCulture);
        return query;
    }
}
