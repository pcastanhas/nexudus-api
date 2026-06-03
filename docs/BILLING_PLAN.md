# Billing — chunked delivery plan

Billing has 50 entities. **21 are done** (chunks 1-3 complete); the remaining 29 are in chunks 4-9
below. Each session grabs the **next chunk whose status is `pending`**, generates those entities,
builds clean, marks the chunk `done` in `manifest.json`, and commits. Chunks are sized by complexity
(small join/history records pack more per chunk; large entities pack fewer).

`manifest.json` is the machine-readable source of truth (`groups[Billing].chunks`); this file is the
human view. Keep them in sync.

## Progress: 21 / 50

Done so far:
- **Baseline:** Charge, DiscountCode, ExtraService, FinancialAccount, PaymentGateway, Product
- **Chunk 1:** Tariff, TariffProduct, TariffTimePass, TariffBookingCredit, TariffSignupProduct
- **Chunk 2:** TariffExtraService, TariffDefaultDueDate, TimePass, TimePassPrice, TimePassTimeSlot
- **Chunk 3:** ProductBookingCredit, ProductExtraService, ProductTimePass, ExtraServicePrice, ExtraServiceTimeSlot

## Chunks

| # | Status | Theme | Entities | Notes |
|---|--------|-------|----------|-------|
| 1 | done | Tariff core + join tables | Tariff, TariffProduct, TariffTimePass, TariffBookingCredit, TariffSignupProduct | Tariff is large; joins are tiny |
| 2 | done | Tariff extras + TimePass family | TariffExtraService, TariffDefaultDueDate, TimePass, TimePassPrice, TimePassTimeSlot | |
| 3 | done | Product & ExtraService children | ProductBookingCredit, ProductExtraService, ProductTimePass, ExtraServicePrice, ExtraServiceTimeSlot | |
| 4 | pending | Contract family | ContractContact, ContractDeposit, ContractPausedPeriod, ContractProduct, ContractSchedule | next up |
| 5 | pending | Coworker subscriptions & usage | CoworkerContract, CoworkerProduct, CoworkerTimePass, CoworkerExtraService, CoworkerExtraServiceUseHistory | CoworkerContract is large |
| 6 | pending | Coworker credits, payments, tokens | CoworkerBookingCredit, CoworkerBookingCreditUseHistory, CoworkerDiscountCode, CoworkerPaymentMethod, CoworkerInvoiceHistory, CoworkerInvoicePaymentToken, CoworkerLedgerEntry | 7 lightweight records; CoworkerLedgerEntry = payments ledger (typed PaymentProvider enum recommended) |
| 7 | pending | Proposal family | Proposal, ProposalContract, ProposalContractSchedule, ProposalProduct, ProposalSchedule | Proposal is large |
| 8 | pending | Standalone records | BasketSession, BusinessCharge, ResourceProduct | |
| 9 | pending | Read-only entities | Invoice, LedgerEntry, CoworkerInvoice, CoworkerInvoiceLine | **First** add `ReadOnlyEndpoint<T>` to core (Search/GetOne/GetMultiple/Update only). Confirm verbs from docs |

## Why chunk 9 is last
The read-only entities need a small core change (a `ReadOnlyEndpoint<T>` base so we don't expose
`Create`/`Delete` methods that would only fail). Isolating them in the final chunk keeps that
infrastructure change in one commit, separate from the routine entity generation.
