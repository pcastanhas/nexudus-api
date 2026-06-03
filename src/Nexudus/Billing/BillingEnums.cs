namespace Nexudus.Billing;

/// <summary>Billing charge period (<c>eChargePeriod</c>).</summary>
public enum ChargePeriod
{
    Minutes = 1,
    Days = 2,
    Weeks = 3,
    Months = 4,
    Uses = 5,
    FourWeekMonths = 6
}

/// <summary>Last-minute dynamic-pricing discount type (<c>eLastMinuteDiscountType</c>).</summary>
public enum LastMinuteDiscountType
{
    Disabled = 1,
    Fixed = 2,
    Gradual = 3
}

/// <summary>Renewal/allowance period (<c>eTimeSpanWeekMonth</c>).</summary>
public enum TimeSpanWeekMonth
{
    Week = 1,
    CalendarMonth = 2,
    TariffMonth = 3,
    Year = 4,
    Day = 5
}

/// <summary>Role of a virtual-office contract contact (<c>eContractContactType</c>).</summary>
public enum ContractContactType
{
    None = 0,
    Director = 1,
    CompanyAlias = 2,
    NominatedRecipient = 3
}

/// <summary>Anti-money-laundering check status for a contract contact (<c>eAmlCheckStatus</c>).</summary>
public enum AmlCheckStatus
{
    NotStarted = 0,
    Pending = 1,
    Clear = 2,
    PotentialMatch = 3,
    ConfirmedMatch = 4,
    Error = 5,
    ManuallyCleared = 6
}

/// <summary>Recurrence pattern for a recurrent charge (<c>eRecurrentChargePattern</c>).</summary>
public enum RecurrentChargePattern
{
    PricePlan = 1,
    Day = 2,
    Week = 3,
    Month = 4,
    Year = 5,
    LastDayOfMonth = 6
}

/// <summary>
/// Payment provider that processed a transaction (<c>ePaymentProvider</c>). This is distinct from
/// <c>ePaymentGatewayType</c> (see <c>PaymentGateway.PaymentGatewayType</c>). Only the values documented
/// for the customer payment-method/ledger entities are mapped here; the full provider set is larger, so
/// other providers may appear as undefined numeric values (which still round-trip safely).
/// </summary>
public enum PaymentProvider
{
    None = 0,
    Stripe = 2,
    StripeACH = 11,
    GoCardless = 12,
    StripeBACS = 13
}

/// <summary>Lifecycle status of a proposal (<c>eProposalStatus</c>).</summary>
public enum ProposalStatus
{
    Draft = 1,
    Sent = 2,
    Accepted = 3,
    Rejected = 4
}
