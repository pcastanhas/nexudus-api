# HANDOFF — read this first each session

This repo is built **incrementally across sessions** in a sandbox whose filesystem is wiped
between sessions. **This repository is the only persistent state.** Treat `manifest.json` as the
source of truth for what's done and what's next, and update it every session.

## Start-of-session checklist
1. Clone the repo (the assistant's sandbox starts empty each session).
2. Install the .NET 8 SDK in the sandbox (`apt-get install -y dotnet-sdk-8.0` from the Ubuntu archive).
3. Read `manifest.json` → pick the next `pending` / `in-progress` group or entity.
4. Read `docs/CODEGEN.md` for the exact file recipe.
5. Read the relevant Nexudus doc pages for the entity's fields and resource path.

## Per-entity workflow
1. Enumerate the group's entities from `https://learn.nexudus.com/llms.txt` (filter by the group's
   `docPathPrefix`) and record them in `manifest.json` with `status: pending`.
2. For each entity, read its `get-…-by-id` doc page for the full field list and the `post-…` page
   for required-vs-optional create fields.
3. Generate `Models/<Entity>.cs` and `API/<Entity>sApi.cs` (see `docs/CODEGEN.md`).
4. `dotnet build Nexudus.sln -c Release` — must be 0 warnings / 0 errors.
5. Mark the entity `done` in `manifest.json`.

## End-of-session checklist
1. Build clean.
2. Update `manifest.json` statuses and this file's "Current status" below.
3. Commit with a clear message and push.

## Conventions
- One project → one DLL (`Nexudus.dll`). Folders mirror namespaces: `src/Nexudus/<Group>/<Entity>/{Models,API}`.
- Namespaces: `Nexudus.<Group>.<Entity>.Models` and `Nexudus.<Group>.<Entity>.API`.
- Models inherit `NexudusEntity`; API classes inherit `NexudusEndpoint<TModel>` and add the
  entity-named methods (`Search…`, `GetOne…`, `GetMultiple…`, `Create…`, `Update…`, `Delete…`).
- Property names match the JSON 1:1 (PascalCase) — no serialization attributes needed.
- No external NuGet packages. Core only: `HttpClient` + `System.Text.Json`.

## PAT handling (security)
The PAT is provided per-session and is short-lived/fine-grained (scope: **Contents: Read and write**
on this repo only). It is used **only** for git over HTTPS, is kept in an environment variable,
is never printed, and is never committed. `.gitignore` excludes `*.pat`, `.env`, and `secrets.*`.

## Current status
- Core library: **complete** (client, auth + refresh, paging, search/filter, command results, errors, streaming).
- **Billing: 6 / 50 entities done** — Charge (+ ChargeFilter), Product, Tariff-adjacent pricing
  pieces still pending. Done so far: Charge, DiscountCode, ExtraService, FinancialAccount,
  PaymentGateway, Product. Full inventory + per-entity status is in `manifest.json`.
- Decisions locked: System group namespace = `Nexudus.SystemApi`; CRM = `Nexudus.Crm`.
- Next up (Billing): work through the 44 pending entities in batches. Before generating the
  read-only/partial ones (Invoice, LedgerEntry, CoworkerInvoice, CoworkerInvoiceLine, *UseHistory),
  confirm which verbs exist and consider adding a `ReadOnlyEndpoint<T>` / partial base to core so
  unsupported methods aren't exposed.
- After Billing: Authentication → SystemApi → Security → Apps → CRM → Spaces → Community →
  Collaboration → Content.
