using System.Globalization;
using BcpRecordNexusActivity.Journal;

namespace BcpRecordNexusActivity.Io;

public interface IWorkbookWriter
{
    /// <summary>Writes the success workbook: sheets "Invoices" and "Payments", mirroring the posted rows.</summary>
    void WriteJournalWorkbook(string path, IReadOnlyList<JournalRow> invoiceRows, IReadOnlyList<JournalRow> paymentRows);

    /// <summary>Writes the failure workbook: sheet "Errors" listing each mapping/validation problem.</summary>
    void WriteErrorWorkbook(string path, IReadOnlyList<BuildError> errors);
}

/// <summary>Lays out the JOURNAL rows / errors and delegates to <see cref="XlsxWriter"/>.</summary>
public sealed class XlsxWorkbookWriter : IWorkbookWriter
{
    private const string DateFormat = "MM/dd/yyyy";

    private static readonly string[] JournalHeaders =
    {
        "PERIOD", "REF", "SOURCE", "SITEID", "ITEM", "ENTITYID", "ACCTNUM", "DEPARTMENT", "AMT",
        "DESCRPN", "ENTRDATE", "REVERSAL", "STATUS", "OEXCHGREF", "BASIS", "LASTDATE", "USERID", "QUICKREVERSAL"
    };

    public void WriteJournalWorkbook(string path, IReadOnlyList<JournalRow> invoiceRows, IReadOnlyList<JournalRow> paymentRows)
    {
        var sheets = new List<XlsxSheet>
        {
            new("Invoices", JournalHeaders, invoiceRows.Select(ToCells).ToList()),
            new("Payments", JournalHeaders, paymentRows.Select(ToCells).ToList())
        };
        XlsxWriter.Write(path, sheets);
    }

    public void WriteErrorWorkbook(string path, IReadOnlyList<BuildError> errors)
    {
        var headers = new[] { "Type", "InvoiceNumber", "FinancialAccountCode", "Reason" };
        var rows = errors
            .Select(e => (IReadOnlyList<object?>)new object?[] { e.Kind, e.Reference, e.FinancialAccountCode, e.Reason })
            .ToList();
        XlsxWriter.Write(path, new[] { new XlsxSheet("Errors", headers, rows) });
    }

    private static IReadOnlyList<object?> ToCells(JournalRow r) => new object?[]
    {
        r.Period,                                   // text, preserves leading/format
        r.Ref,
        r.Source,
        r.SiteId,
        r.Item,                                     // number
        r.EntityId,
        r.AcctNum,
        r.Department,
        r.Amt,                                      // number, 2 dp
        r.Descrpn,
        r.EntrDate.ToString(DateFormat, CultureInfo.InvariantCulture),
        r.Reversal,
        r.Status,
        r.OExchgRef,
        r.Basis,
        r.LastDate.ToString(DateFormat, CultureInfo.InvariantCulture),
        r.UserId,
        r.QuickReversal
    };
}
