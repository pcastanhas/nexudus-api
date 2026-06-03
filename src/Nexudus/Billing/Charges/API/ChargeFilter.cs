namespace Nexudus.Billing.Charges.API;

/// <summary>
/// Strongly-typed, fluent filters for <c>SearchCharges</c>. These map to the API's documented
/// <c>Charge_*</c> / <c>from_</c> / <c>to_</c> query parameters. A representative subset is provided;
/// any other documented filter can be added with the inherited <see cref="SearchParameters.Where(string, string)"/>.
/// </summary>
public sealed class ChargeFilter : SearchParameters
{
    public ChargeFilter ForCoworker(long coworkerId) { Where("Charge_Coworker", coworkerId); return this; }
    public ChargeFilter ForBusiness(long businessId) { Where("Charge_Business", businessId); return this; }
    public ChargeFilter WithChargeNumber(string chargeNumber) { Where("Charge_ChargeNumber", chargeNumber); return this; }
    public ChargeFilter IsInvoiced(bool invoiced = true) { Where("Charge_Invoiced", invoiced); return this; }
    public ChargeFilter IsRegularCharge(bool regular = true) { Where("Charge_RegularCharge", regular); return this; }

    public ChargeFilter TotalAmountBetween(decimal from, decimal to)
    {
        Where("from_Charge_TotalAmount", from);
        Where("to_Charge_TotalAmount", to);
        return this;
    }

    public ChargeFilter SaleDateBetween(DateTimeOffset from, DateTimeOffset to)
    {
        Where("from_Charge_SaleDate", from);
        Where("to_Charge_SaleDate", to);
        return this;
    }

    public ChargeFilter CreatedBetween(DateTimeOffset from, DateTimeOffset to)
    {
        Where("from_Charge_CreatedOn", from);
        Where("to_Charge_CreatedOn", to);
        return this;
    }

    public ChargeFilter UpdatedBetween(DateTimeOffset from, DateTimeOffset to)
    {
        Where("from_Charge_UpdatedOn", from);
        Where("to_Charge_UpdatedOn", to);
        return this;
    }
}
