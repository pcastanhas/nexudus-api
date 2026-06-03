namespace Nexudus.Billing.CoworkerInvoices.Models;

/// <summary>
/// An invoice document issued to a customer. Tracks amounts owed, payment status, billing details, and
/// integration state with external accounting systems. May be a draft (still editable), a finalised invoice,
/// or a credit note (<see cref="CreditNote"/>) referencing the original via <see cref="OriginalInvoiceGuid"/>.
/// <para>
/// The API supports search, get, and update for customer invoices but not create or delete, so this endpoint
/// derives from <see cref="ReadOnlyEndpoint{T}"/>. The denormalised <see cref="CoworkerRegularPaymentProvider"/>
/// is returned as a display string here (not the <c>PaymentProvider</c> enum). Fields below the
/// <see cref="NexudusEntity"/> base mirror the "Get one CoworkerInvoice" response.
/// </para>
/// </summary>
public sealed class CoworkerInvoice : NexudusEntity
{
    public int CoworkerId { get; set; }
    public string? CoworkerFullName { get; set; }
    public string? CoworkerRegularPaymentContractNumber { get; set; }
    public string? CoworkerRegularPaymentProvider { get; set; }
    public string? CoworkerCardNumber { get; set; }
    public string? CoworkerGoCardlessContractNumber { get; set; }
    public bool? CoworkerEnableGoCardlessPayments { get; set; }
    public string? CoworkerBillingEmail { get; set; }
    public bool? CoworkerNotifyOnNewInvoice { get; set; }
    public bool? CoworkerNotifyOnNewPayment { get; set; }
    public bool? CoworkerNotifyOnFailedPayment { get; set; }
    public bool? CoworkerDoNotProcessInvoicesAutomatically { get; set; }
    public string? CoworkerCompanyName { get; set; }
    public string? CoworkerTeamNames { get; set; }
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? PaymentReference { get; set; }
    public string? BillToName { get; set; }
    public string? BillToAddress { get; set; }
    public string? BillToCity { get; set; }
    public string? BillToPostCode { get; set; }
    public string? BillToPhone { get; set; }
    public string? BillToFax { get; set; }
    public string? BillToState { get; set; }
    public int BillToCountryId { get; set; }
    public string? BillToCountryName { get; set; }
    public string? BillToCountryTwoDigitsCode { get; set; }
    public string? BillToBankAccount { get; set; }
    public string? BillToTaxIDNumber { get; set; }
    public string? PurchaseOrder { get; set; }
    public string? Description { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? InvoiceFromDate { get; set; }
    public DateTimeOffset? InvoiceToDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? PaidAmount { get; set; }
    public decimal? TransactionTotalAmount { get; set; }
    public int? TransactionCurrencyId { get; set; }
    public string? TransactionCurrencyCode { get; set; }
    public decimal? TransactionExchangeRate { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal TaxAmount { get; set; }
    public bool Draft { get; set; }
    public bool Void { get; set; }
    public bool WaitingForInvoiceNumber { get; set; }
    public bool Paid { get; set; }
    public bool Sent { get; set; }
    public DateTimeOffset? SentOn { get; set; }
    public DateTimeOffset? PaidOn { get; set; }
    public bool Refunded { get; set; }
    public bool XeroInvoiceTransfered { get; set; }
    public bool XeroPaymentTransfered { get; set; }
    public bool QuickbooksInvoiceTransfered { get; set; }
    public bool QuickbooksPaymentTransfered { get; set; }
    public bool MoloniInvoiceTransferred { get; set; }
    public bool MoloniPaymentTransferred { get; set; }
    public StorecoveInvoiceStatus StorecoveInvoiceStatus { get; set; }
    public bool AutoTransferToStorecove { get; set; }
    public string? StorecoveUniqueId { get; set; }
    public DateTimeOffset? RefundedOn { get; set; }
    public bool CreditNote { get; set; }
    public string? OriginalInvoiceGuid { get; set; }
    public string? ContractGuid { get; set; }
    public string? CustomData { get; set; }
    public string? GoCardlessReference { get; set; }
    public string? SpreedlyToken { get; set; }
    public DateTimeOffset? LastPaymentAttempt { get; set; }
    public bool Billed { get; set; }
    public bool DoNotApplyCreditAutomatically { get; set; }
    public DateTimeOffset? CreatedOnLocal { get; set; }
    public DateTimeOffset? DueDateLocal { get; set; }
    public DateTimeOffset? InvoiceFromDateLocal { get; set; }
    public DateTimeOffset? InvoiceToDateLocal { get; set; }
    public DateTimeOffset? PaidOnLocal { get; set; }
    public DateTimeOffset? RefundedOnLocal { get; set; }
    public DateTimeOffset? LastPaymentAttemptLocal { get; set; }
    public decimal? ReceivedAmount { get; set; }
    public decimal? CreditedAmount { get; set; }
    public decimal? RefundedAmount { get; set; }
    public string? NexKioskTransactionId { get; set; }
    public bool AutoTransferToXeroOrQuickBooks { get; set; }
}
