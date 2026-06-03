namespace Nexudus.Billing.FinancialAccounts.Models;

/// <summary>A bookkeeping account used for categorising revenue and payments.</summary>
public sealed class FinancialAccount : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? Name { get; set; }

    /// <summary>Short reference code, typically matching an external accounting system.</summary>
    public string? Code { get; set; }

    public string? Description { get; set; }

    /// <summary>Category of the account (Sales / Payments / Deposits).</summary>
    public FinancialAccountType AccountType { get; set; }
}

/// <summary>Category of a <see cref="FinancialAccount"/> (<c>eFinancialAccountType</c>).</summary>
public enum FinancialAccountType
{
    None = 0,
    Sales = 1,
    Payments = 2,
    Deposits = 3
}
