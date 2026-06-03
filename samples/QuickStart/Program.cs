using Nexudus;
using Nexudus.Billing.Charges.API;
using Nexudus.Billing.Charges.Models;

// 1. Connect (username/password; the client acquires and refreshes the bearer token for you).
using var client = NexudusClient.WithPassword("admin@example.com", "your-password");
// Or with a token you already have:  using var client = NexudusClient.WithToken("eyJ...");

var charges = new ChargesApi(client);

// 2. Search with strongly-typed pagination + filters.
var page = await charges.SearchCharges(
    new ChargeFilter { Page = 1, Size = 25 }
        .ForBusiness(123)
        .IsInvoiced(false)
        .CreatedBetween(DateTimeOffset.UtcNow.AddMonths(-1), DateTimeOffset.UtcNow)
        .SortBy("CreatedOn", SortDirection.Descending));

Console.WriteLine($"{page.TotalItems} matching charges; showing {page.Records.Count}.");
foreach (Charge c in page.Records)
    Console.WriteLine($"  #{c.Id} {c.Description} = {c.TotalAmount} {c.BusinessCurrencyCode}");

// 3. Get one (full record) and a batch.
Charge? one = await charges.GetOneCharge(87654321);
IReadOnlyList<Charge> many = await charges.GetMultipleCharges(87654321, 87654322);

// 4. Create.
long newId = await charges.CreateCharge(new Charge
{
    CoworkerId = 456,
    BusinessId = 123,
    Quantity = 1,
    DiscountAmount = 0m,
    CreditAmount = 0m,
    TotalAmount = 49.99m,
    Description = "One-off charge",
});
Console.WriteLine($"Created charge #{newId}");

// 5. Update — fetch the full record first, change it, send it back whole.
Charge? toEdit = await charges.GetOneCharge(newId);
if (toEdit is not null)
{
    toEdit.Description = "One-off charge (updated)";
    CommandResult update = await charges.UpdateCharge(toEdit);
    Console.WriteLine($"Update ok: {update.WasSuccessful}");
}

// 6. Delete.
await charges.DeleteCharge(newId);

// 7. Stream every matching record across all pages.
await foreach (Charge c in charges.EnumerateCharges(new ChargeFilter().IsInvoiced(true)))
    Console.WriteLine(c.Id);

// 8. Errors (e.g. validation) surface as NexudusApiException.
try
{
    await charges.CreateCharge(new Charge { CoworkerId = 0, BusinessId = 0 });
}
catch (NexudusApiException ex)
{
    Console.WriteLine(ex.Message);
    foreach (var e in ex.Errors)
        Console.WriteLine($"  {e.PropertyName}: {e.Message}");
}
