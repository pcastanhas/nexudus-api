using System.Globalization;
using System.IO.Compression;
using System.Security;
using System.Text;

namespace BcpRecordNexusActivity.Io;

/// <summary>A single worksheet: a name, a header row, and data rows. Each cell is a string (written as an
/// inline string) or a numeric value (int/long/decimal/double, written as a number cell).</summary>
public sealed record XlsxSheet(string Name, IReadOnlyList<string> Headers, IReadOnlyList<IReadOnlyList<object?>> Rows);

/// <summary>
/// Minimal SpreadsheetML writer: enough of the OOXML format to emit a multi-sheet <c>.xlsx</c> with inline
/// strings and number cells, using only the BCL (no third-party Excel library). Validated by reading the
/// output back with a standard parser.
/// </summary>
public static class XlsxWriter
{
    public static void Write(string path, IReadOnlyList<XlsxSheet> sheets)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        Write(stream, sheets);
    }

    public static void Write(Stream output, IReadOnlyList<XlsxSheet> sheets)
    {
        using var zip = new ZipArchive(output, ZipArchiveMode.Create, leaveOpen: true);

        WriteEntry(zip, "[Content_Types].xml", ContentTypes(sheets.Count));
        WriteEntry(zip, "_rels/.rels", RootRels());
        WriteEntry(zip, "xl/workbook.xml", Workbook(sheets));
        WriteEntry(zip, "xl/_rels/workbook.xml.rels", WorkbookRels(sheets.Count));

        for (var i = 0; i < sheets.Count; i++)
            WriteEntry(zip, $"xl/worksheets/sheet{i + 1}.xml", Worksheet(sheets[i]));
    }

    private static void WriteEntry(ZipArchive zip, string name, string content)
    {
        var entry = zip.CreateEntry(name, CompressionLevel.Optimal);
        using var writer = new StreamWriter(entry.Open(), new UTF8Encoding(false));
        writer.Write(content);
    }

    private static string ContentTypes(int sheetCount)
    {
        var sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        sb.Append("<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">");
        sb.Append("<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>");
        sb.Append("<Default Extension=\"xml\" ContentType=\"application/xml\"/>");
        sb.Append("<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>");
        for (var i = 0; i < sheetCount; i++)
            sb.Append($"<Override PartName=\"/xl/worksheets/sheet{i + 1}.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>");
        sb.Append("</Types>");
        return sb.ToString();
    }

    private static string RootRels() =>
        "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>"
        + "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">"
        + "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>"
        + "</Relationships>";

    private static string Workbook(IReadOnlyList<XlsxSheet> sheets)
    {
        var sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        sb.Append("<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" ");
        sb.Append("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"><sheets>");
        for (var i = 0; i < sheets.Count; i++)
            sb.Append($"<sheet name=\"{Escape(sheets[i].Name)}\" sheetId=\"{i + 1}\" r:id=\"rId{i + 1}\"/>");
        sb.Append("</sheets></workbook>");
        return sb.ToString();
    }

    private static string WorkbookRels(int sheetCount)
    {
        var sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        sb.Append("<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">");
        for (var i = 0; i < sheetCount; i++)
            sb.Append($"<Relationship Id=\"rId{i + 1}\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet{i + 1}.xml\"/>");
        sb.Append("</Relationships>");
        return sb.ToString();
    }

    private static string Worksheet(XlsxSheet sheet)
    {
        var sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
        sb.Append("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"><sheetData>");

        var rowIndex = 1;
        AppendRow(sb, rowIndex++, sheet.Headers.Cast<object?>().ToList());
        foreach (var row in sheet.Rows)
            AppendRow(sb, rowIndex++, row);

        sb.Append("</sheetData></worksheet>");
        return sb.ToString();
    }

    private static void AppendRow(StringBuilder sb, int rowIndex, IReadOnlyList<object?> cells)
    {
        sb.Append($"<row r=\"{rowIndex}\">");
        for (var c = 0; c < cells.Count; c++)
        {
            var reference = $"{ColumnName(c)}{rowIndex}";
            var value = cells[c];

            if (value is null)
            {
                sb.Append($"<c r=\"{reference}\"/>");
            }
            else if (IsNumber(value, out var number))
            {
                sb.Append($"<c r=\"{reference}\"><v>{number}</v></c>");
            }
            else
            {
                sb.Append($"<c r=\"{reference}\" t=\"inlineStr\"><is><t xml:space=\"preserve\">{Escape(value.ToString() ?? "")}</t></is></c>");
            }
        }
        sb.Append("</row>");
    }

    private static bool IsNumber(object value, out string formatted)
    {
        switch (value)
        {
            case decimal d:
                formatted = d.ToString("0.##", CultureInfo.InvariantCulture);
                return true;
            case int i:
                formatted = i.ToString(CultureInfo.InvariantCulture);
                return true;
            case long l:
                formatted = l.ToString(CultureInfo.InvariantCulture);
                return true;
            case double db:
                formatted = db.ToString("0.########", CultureInfo.InvariantCulture);
                return true;
            default:
                formatted = "";
                return false;
        }
    }

    /// <summary>0 -> A, 25 -> Z, 26 -> AA, ...</summary>
    private static string ColumnName(int index)
    {
        var name = "";
        index++;
        while (index > 0)
        {
            var rem = (index - 1) % 26;
            name = (char)('A' + rem) + name;
            index = (index - 1) / 26;
        }
        return name;
    }

    private static string Escape(string s) => SecurityElement.Escape(s) ?? s;
}
