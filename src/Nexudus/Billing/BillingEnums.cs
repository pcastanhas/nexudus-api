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
