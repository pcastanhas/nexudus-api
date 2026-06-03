# Nexudus .NET Client

A strongly-typed C# client for the [Nexudus REST API](https://learn.nexudus.com/rest-api/overview).
No raw HTTP, no hand-written JSON — call typed methods and get back typed models.

```csharp
using var client = NexudusClient.WithPassword("admin@example.com", "pw");
var charges = new Nexudus.Billing.Charges.API.ChargesApi(client);

var page = await charges.SearchCharges(
    new ChargeFilter { Page = 1, Size = 25 }.ForBusiness(123).IsInvoiced(false));

var charge = await charges.GetOneCharge(87654321);
long id     = await charges.CreateCharge(new Charge { CoworkerId = 456, BusinessId = 123, Quantity = 1, TotalAmount = 49.99m });
await charges.UpdateCharge(charge!);     // send the full record back
await charges.DeleteCharge(id);
```

## Namespace layout

Exactly as requested — each entity lives under its section and subsection:

```
Nexudus                              core: client, auth, paging, errors (reusable)
Nexudus.Billing.Charges.Models       Charge
Nexudus.Billing.Charges.API          ChargesApi, ChargeFilter
```

`ChargesApi` exposes `SearchCharges`, `GetOneCharge`, `GetMultipleCharges`,
`CreateCharge`, `UpdateCharge`, `DeleteCharge` (plus `EnumerateCharges` to stream
across pages). All methods are async (`Task<...>`) and take an optional
`CancellationToken`.

## What the core handles for you

- **Auth** — password grant (with optional `totp`), automatic bearer-token refresh, and a
  one-time retry on `401`. Or pass a token directly with `NexudusClient.WithToken(...)`.
- **Paging** — `PagedResult<T>` (`Records`, `TotalItems`, `HasNextPage`, …) and
  `EnumerateAsync` / `EnumerateCharges` to walk every page automatically.
- **Search** — typed `Page` / `Size` / `OrderBy` / `Direction`, plus the API's
  `Entity_Field` / `from_` / `to_` filters via `ChargeFilter` or the `Where(...)` escape hatch.
- **Writes** — create returns the new `Id`; update sends the complete record (the API has no
  PATCH, so partial updates would clear fields — always edit a record fetched via `GetOneCharge`).
- **Errors** — non-success responses and `WasSuccessful: false` throw `NexudusApiException`,
  with `StatusCode` and the field-level `Errors` list populated.

## Adding another entity

Every entity in the API uses the identical shape, so each new one is two small files.
Example — **Billing › Products** (`/api/billing/products`):

**1. Model** — `Nexudus/Billing/Products/Models/Product.cs`
```csharp
namespace Nexudus.Billing.Products.Models;

public sealed class Product : NexudusEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    // ...one property per field from the "Get one Product" doc page...
}
```

**2. API** — `Nexudus/Billing/Products/API/ProductsApi.cs`
```csharp
using Nexudus.Billing.Products.Models;

namespace Nexudus.Billing.Products.API;

public sealed class ProductsApi : NexudusEndpoint<Product>
{
    public ProductsApi(NexudusClient client) : base(client) { }
    protected override string ResourcePath => "billing/products";

    public Task<PagedResult<Product>> SearchProducts(SearchParameters? p = null, CancellationToken ct = default) => SearchAsync(p, ct);
    public Task<Product?> GetOneProduct(long id, CancellationToken ct = default)                                 => GetOneAsync(id, ct);
    public Task<IReadOnlyList<Product>> GetMultipleProducts(IEnumerable<long> ids, CancellationToken ct = default) => GetManyAsync(ids, ct);
    public Task<long> CreateProduct(Product r, CancellationToken ct = default)                                   => CreateAsync(r, ct);
    public Task<CommandResult> UpdateProduct(Product r, CancellationToken ct = default)                          => UpdateAsync(r, ct);
    public Task<CommandResult> DeleteProduct(long id, CancellationToken ct = default)                            => DeleteAsync(id, ct);
}
```

That's the whole recipe: inherit `NexudusEndpoint<T>`, set `ResourcePath`, and add the
entity-named wrappers. The model's property names match the JSON one-to-one (PascalCase),
so serialization needs no attributes.

> Because the model fields come straight from each doc page and the rest is mechanical, this is
> a strong candidate for code generation (read `llms.txt` → fetch each `*-by-id.md` for the field
> list → emit the two files). Worth doing if you want the full surface rather than the sections
> you actually use.

## Requirements

.NET 8+. No external NuGet packages — everything uses the built-in `HttpClient` and
`System.Text.Json`. Built and verified with the .NET 8 SDK.
