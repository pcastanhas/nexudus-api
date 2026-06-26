namespace BcpRecordNexusActivity.Journal;

/// <summary>Fixed JOURNAL column values for this interface (per the agreed spec / sample files).</summary>
public static class JournalConstants
{
    public const string Source = "NX";
    public const string SiteId = "@";
    public const string Department = "@";
    public const string Reversal = "N";
    public const string Status = "P";
    public const string Basis = "B";
    public const string OExchgRef = "";
    public const string UserId = "IMPORT";
    public const string QuickReversal = "0";

    public const int DescriptionMaxLength = 75;

    /// <summary>JOURNAL.REF = "N" + last 4 of the posting period + a 3-digit sequence (8 chars, fits REF varchar(8)).</summary>
    public const int MaxSequence = 999;

    public static string MakeRef(string postingPeriod, int sequence)
    {
        if (sequence is < 1 or > MaxSequence)
            throw new InvalidOperationException(
                $"Journal sequence {sequence} is out of range 1..{MaxSequence}; REF would overflow varchar(8).");
        var last4 = postingPeriod.Length <= 4 ? postingPeriod : postingPeriod[^4..];
        return $"N{last4}{sequence:D3}";
    }
}

/// <summary>One JOURNAL row (the 18 columns shared by the sample import files, in order).</summary>
public sealed record JournalRow(
    string Period,
    string Ref,
    string Source,
    string SiteId,
    int Item,
    string EntityId,
    string AcctNum,
    string Department,
    decimal Amt,
    string Descrpn,
    DateTime EntrDate,
    string Reversal,
    string Status,
    string OExchgRef,
    string Basis,
    DateTime LastDate,
    string UserId,
    string QuickReversal);
