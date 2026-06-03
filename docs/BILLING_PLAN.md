# Billing — chunked delivery plan

Billing has 50 entities. 6 are done; the remaining 44 are grouped into 9 ordered chunks below.
Each session grabs the **next chunk whose status is `pending`**, generates those entities, builds
clean, marks the chunk `done` in `manifest.json`, and commits. Chunks are sized by complexity
(small join/history records pack more per chunk; large entities pack fewer).

`manifest.json` is the machine-readable source of truth (`groups[Billing].chunks`); this file is the
human view. Keep them in sync.

## Done (6)
Charge · DiscountCode · ExtraService · FinancialAccount · PaymentGateway · Product

## Pending chunks (44)

| # | Theme | Entities | Notes |
|---|-------|----------|-------|
| 1 | Tariff core + join tables | Tariff, TariffProduct, TariffTimePass, TariffBookingCredit, TariffSignupProduct | Tariff is large; joins are tiny |
| 2 | Tariff extras + TimePass family | TariffExtraService, TariffDefaultDueDate, TimePass, TimePassPrice, TimePassTimeSlot | |
| 3 | Product & ExtraService children | ProductBookingCredit, ProductExtraService, ProductTimePass, ExtraServicePrice, ExtraServiceTimeSlot | |
| 4 | Contract family | ContractContact, ContractDeposit, ContractPausedPeriod, ContractProduct, ContractSchedule | |
| 5 | Coworker subscriptions & usage | CoworkerContract, CoworkerProduct, CoworkerTimePass, CoworkerExtraService, CoworkerExtraServiceUseHistory | CoworkerContract is large |
| 6 | Coworker credits, payments, tokens | CoworkerBookingCredit, CoworkerBookingCreditUseHistory, CoworkerDiscountCode, CoworkerPaymentMethod, CoworkerInvoiceHistory, CoworkerInvoicePaymentToken, CoworkerLedgerEntry | 7 lightweight records |
| 7 | Proposal family | Proposal, ProposalContract, ProposalContractSchedule, ProposalProduct, ProposalSchedule | Proposal is large |
| 8 | Standalone records | BasketSession, BusinessCharge, ResourceProduct | |
| 9 | Read-only entities | Invoice, LedgerEntry, CoworkerInvoice, CoworkerInvoiceLine | **First** add `ReadOnlyEndpoint<T>` to core (Search/GetOne/GetMultiple/Update only). Confirm verbs from docs |

## Why chunk 9 is last
The read-only entities need a small core change (a `ReadOnlyEndpoint<T>` base so we don't expose
`Create`/`Delete` methods that would only fail). Isolating them in the final chunk keeps that
infrastructure change in one commit, separate from the routine entity generation.
