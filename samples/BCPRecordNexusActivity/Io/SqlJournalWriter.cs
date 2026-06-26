using BcpRecordNexusActivity.Journal;
using Microsoft.Data.SqlClient;

namespace BcpRecordNexusActivity.Io;

public interface IJournalWriter
{
    /// <summary>Inserts every row into JOURNAL atomically (all or nothing). Returns the rows inserted.</summary>
    Task<int> WriteAsync(IReadOnlyList<JournalRow> rows, CancellationToken cancellationToken = default);
}

/// <summary>
/// Inserts JOURNAL rows into the MRI (test) database. All rows for the run go in one transaction, so a
/// failure rolls the whole batch back and surfaces as the failure-email path — nothing is half-posted.
/// Only the 18 columns from the agreed layout are written; MRI supplies defaults/NULL for the rest.
/// </summary>
public sealed class SqlJournalWriter : IJournalWriter
{
    private readonly string _connectionString;

    public SqlJournalWriter(string connectionString) => _connectionString = connectionString;

    private const string InsertSql = """
        INSERT INTO JOURNAL
            (PERIOD, REF, SOURCE, SITEID, ITEM, ENTITYID, ACCTNUM, DEPARTMENT, AMT, DESCRPN,
             ENTRDATE, REVERSAL, STATUS, OEXCHGREF, BASIS, LASTDATE, USERID, QUICKREVERSAL)
        VALUES
            (@PERIOD, @REF, @SOURCE, @SITEID, @ITEM, @ENTITYID, @ACCTNUM, @DEPARTMENT, @AMT, @DESCRPN,
             @ENTRDATE, @REVERSAL, @STATUS, @OEXCHGREF, @BASIS, @LASTDATE, @USERID, @QUICKREVERSAL);
        """;

    public async Task<int> WriteAsync(IReadOnlyList<JournalRow> rows, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            await using var command = new SqlCommand(InsertSql, connection, transaction);
            DefineParameters(command);

            var count = 0;
            foreach (var row in rows)
            {
                BindParameters(command, row);
                count += await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            }

            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
    }

    private static void DefineParameters(SqlCommand cmd)
    {
        cmd.Parameters.Add("@PERIOD", System.Data.SqlDbType.VarChar, 6);
        cmd.Parameters.Add("@REF", System.Data.SqlDbType.VarChar, 8);
        cmd.Parameters.Add("@SOURCE", System.Data.SqlDbType.VarChar, 2);
        cmd.Parameters.Add("@SITEID", System.Data.SqlDbType.VarChar, 2);
        cmd.Parameters.Add("@ITEM", System.Data.SqlDbType.Int);
        cmd.Parameters.Add("@ENTITYID", System.Data.SqlDbType.VarChar, 6);
        cmd.Parameters.Add("@ACCTNUM", System.Data.SqlDbType.VarChar, 11);
        cmd.Parameters.Add("@DEPARTMENT", System.Data.SqlDbType.VarChar, 1);
        cmd.Parameters.Add("@AMT", System.Data.SqlDbType.Decimal).Precision = 15;
        cmd.Parameters["@AMT"].Scale = 2;
        cmd.Parameters.Add("@DESCRPN", System.Data.SqlDbType.VarChar, 75);
        cmd.Parameters.Add("@ENTRDATE", System.Data.SqlDbType.Date);
        cmd.Parameters.Add("@REVERSAL", System.Data.SqlDbType.VarChar, 1);
        cmd.Parameters.Add("@STATUS", System.Data.SqlDbType.VarChar, 1);
        cmd.Parameters.Add("@OEXCHGREF", System.Data.SqlDbType.VarChar, 8);
        cmd.Parameters.Add("@BASIS", System.Data.SqlDbType.VarChar, 1);
        cmd.Parameters.Add("@LASTDATE", System.Data.SqlDbType.Date);
        cmd.Parameters.Add("@USERID", System.Data.SqlDbType.VarChar, 20);
        cmd.Parameters.Add("@QUICKREVERSAL", System.Data.SqlDbType.VarChar, 1);
    }

    private static void BindParameters(SqlCommand cmd, JournalRow r)
    {
        cmd.Parameters["@PERIOD"].Value = r.Period;
        cmd.Parameters["@REF"].Value = r.Ref;
        cmd.Parameters["@SOURCE"].Value = r.Source;
        cmd.Parameters["@SITEID"].Value = r.SiteId;
        cmd.Parameters["@ITEM"].Value = r.Item;
        cmd.Parameters["@ENTITYID"].Value = r.EntityId;
        cmd.Parameters["@ACCTNUM"].Value = r.AcctNum;
        cmd.Parameters["@DEPARTMENT"].Value = r.Department;
        cmd.Parameters["@AMT"].Value = r.Amt;
        cmd.Parameters["@DESCRPN"].Value = r.Descrpn;
        cmd.Parameters["@ENTRDATE"].Value = r.EntrDate.Date;
        cmd.Parameters["@REVERSAL"].Value = r.Reversal;
        cmd.Parameters["@STATUS"].Value = r.Status;
        cmd.Parameters["@OEXCHGREF"].Value = string.IsNullOrEmpty(r.OExchgRef) ? (object)DBNull.Value : r.OExchgRef;
        cmd.Parameters["@BASIS"].Value = r.Basis;
        cmd.Parameters["@LASTDATE"].Value = r.LastDate.Date;
        cmd.Parameters["@USERID"].Value = r.UserId;
        cmd.Parameters["@QUICKREVERSAL"].Value = r.QuickReversal;
    }
}
