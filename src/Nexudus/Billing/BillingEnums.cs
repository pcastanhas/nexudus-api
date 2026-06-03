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
