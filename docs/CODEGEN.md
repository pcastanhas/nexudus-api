# CODEGEN — adding an entity

Every Nexudus entity exposes the identical 6–7 endpoints, so each one is two small files plus a
manifest update. Use `Billing/Charges` as the reference implementation.

## 1. Find the facts (from the docs)
- Resource path + verbs: the doc URLs encode them, e.g. `rest-api/billing/get-charges-by-id` →
  `GET /api/billing/charges/{id}`. Resource path = `billing/charges`.
- Full field list + types: from the **`get-…-by-id`** page (the single-record response is the
  superset; list/batch responses omit some fields).
- Create body (required vs optional): from the **`post-…`** page.

## 2. Model — `src/Nexudus/<Group>/<Entity>/Models/<Entity>.cs`
```csharp
namespace Nexudus.<Group>.<Entity>.Models;

public sealed class <Entity> : NexudusEntity   // base supplies Id, UniqueId, CreatedOn, UpdatedOn, etc.
{
    // one property per field from the get-by-id page; map types:
    //   integer -> int / int? (nullable when it's a nullable FK or optional)
    //   number  -> decimal (money) ; string -> string? ; boolean -> bool
    //   date/datetime -> DateTimeOffset? ; GUID refs -> string?
}
```
Do **not** redeclare base fields (`Id`, `UniqueId`, `CreatedOn`, `UpdatedOn`, `UpdatedBy`,
`IsNew`, `SystemId`, `ToStringText`, `LocalizationDetails`, `CustomFields`).

## 3. API — `src/Nexudus/<Group>/<Entity>/API/<Entity>sApi.cs`
```csharp
using Nexudus.<Group>.<Entity>.Models;

namespace Nexudus.<Group>.<Entity>.API;

public sealed class <Entity>sApi : NexudusEndpoint<<Entity>>
{
    public <Entity>sApi(NexudusClient client) : base(client) { }
    protected override string ResourcePath => "<group>/<entities>";

    public Task<PagedResult<<Entity>>> Search<Entity>s(SearchParameters? p = null, CancellationToken ct = default) => SearchAsync(p, ct);
    public Task<<Entity>?> GetOne<Entity>(long id, CancellationToken ct = default)                                 => GetOneAsync(id, ct);
    public Task<IReadOnlyList<<Entity>>> GetMultiple<Entity>s(IEnumerable<long> ids, CancellationToken ct = default) => GetManyAsync(ids, ct);
    public Task<long> Create<Entity>(<Entity> r, CancellationToken ct = default)                                   => CreateAsync(r, ct);
    public Task<CommandResult> Update<Entity>(<Entity> r, CancellationToken ct = default)                          => UpdateAsync(r, ct);
    public Task<CommandResult> Delete<Entity>(long id, CancellationToken ct = default)                             => DeleteAsync(id, ct);
}
```
Optional: a `<Entity>Filter : SearchParameters` with fluent helpers for the documented
`<Entity>_Field` / `from_` / `to_` query params (see `ChargeFilter`).

## 4. Verify + record
- `dotnet build Nexudus.sln -c Release` (0 warnings / 0 errors).
- Set the entity `status: done` in `manifest.json`.

## Special cases to watch
- **Non-CRUD endpoints**: some pages are actions (e.g. `…-run-command`, `…-commands`, availability,
  calendar feeds, batch-only). Add these as extra methods on the relevant API class rather than
  forcing the CRUD template.
- **Commands**: entities with `get-…-commands` / `post-…-run-command` support a generic command
  call — consider a shared helper on the core if several groups use it.
- **`Nexudus.System`**: see the note in `manifest.json` before generating that group.
