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
