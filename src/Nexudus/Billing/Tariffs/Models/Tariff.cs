namespace Nexudus.Billing.Tariffs.Models;

/// <summary>
/// A membership plan customers sign up to. Every sign-up generates a <c>CoworkerContract</c>
/// based on this plan's settings. Defines pricing, billing cycle, cancellation rules, usage
/// limits, identity/AML checks, and (for virtual offices) mail-handling preferences.
/// </summary>
public sealed class Tariff : NexudusEntity
{
    public int BusinessId { get; set; }
    public string? BusinessName { get; set; }
    public string? Name { get; set; }

    /// <summary>Category of the plan.</summary>
    public TariffType SystemTariffType { get; set; }

    public decimal Price { get; set; }
    public int? DefaultInvoicingDay { get; set; }
    public bool Visible { get; set; }

    public bool AvailableToAi { get; set; }
    public string? NotesForAi { get; set; }
    public bool ShowPriceForAi { get; set; }
    public decimal? PriceForAi { get; set; }

    public bool UseTimePasses { get; set; }
    public string? Description { get; set; }
    public string? InvoiceLineDisplayAs { get; set; }
    public decimal? SignUpFee { get; set; }

    public int CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public int? TaxRateId { get; set; }
    public int? ReducedTaxRateId { get; set; }
    public int? ExemptTaxRateId { get; set; }
    public int? FinancialAccountId { get; set; }

    public string? TermsAndConditions { get; set; }
    public string? ContractDocumentFileName { get; set; }
    public string? NewContractDocumentUrl { get; set; }
    public bool? ClearContractDocumentFile { get; set; }

    public int CancellationPeriod { get; set; }
    public int DisplayOrder { get; set; }
    public string? GroupName { get; set; }
    public bool DisablePortalCancellations { get; set; }
    public int? SubscribersLimit { get; set; }
    public int? CancellationLimitDays { get; set; }
    public int? DefaultContractTerm { get; set; }

    /// <summary>Days after cancellation before the member account is deactivated (API spelling preserved).</summary>
    public int? CancelMemeberAccountAfter { get; set; }

    public int? CheckinPricePlanLimit { get; set; }
    public int? CheckinMonthLimit { get; set; }
    public int? CheckinWeekLimit { get; set; }
    public int? VisitorMonthLimit { get; set; }
    public int? VisitorWeekLimit { get; set; }
    public int? VisitorDayLimit { get; set; }
    public int? HoursPricePlanLimit { get; set; }
    public int? HoursMonthLimit { get; set; }
    public int? HoursWeekLimit { get; set; }
    public int? BookingMinuteWeekLimit { get; set; }
    public int? BookingMinuteMonthLimit { get; set; }

    public decimal? DiscountExtraServices { get; set; }
    public decimal? DiscountTimePasses { get; set; }
    public decimal? DiscountCharges { get; set; }

    /// <summary>Billing cycle length in months (0 when billing by weeks).</summary>
    public int InvoiceEvery { get; set; }
    /// <summary>Billing cycle length in weeks (0 when billing by months).</summary>
    public int InvoiceEveryWeeks { get; set; }

    public int? AutoCancelAfter { get; set; }
    public int? AdvanceInvoiceCycles { get; set; }
    public int? ProrateDayOfMonth { get; set; }
    public int? ProrateDaysBefore { get; set; }
    public bool ProrateCancellations { get; set; }
    public int? ChargeAndExtend { get; set; }
    public bool? ExcludeFromInvoice { get; set; }
    public bool AutoRaiseInvoices { get; set; }
    public int? RaiseInvoiceEvery { get; set; }
    public int? RaiseInvoiceEveryWeeks { get; set; }

    public decimal? MinimumPrice { get; set; }
    public bool MinimumPriceIncludeTimePasses { get; set; }
    public bool MinimumPriceIncludeExtraServices { get; set; }
    public bool MinimumPriceIncludeEvents { get; set; }

    public bool Archived { get; set; }
    public bool Starred { get; set; }
    public bool KeepNewAccountsOnHold { get; set; }

    public bool CanBePaused { get; set; }
    public int? PauseYearlyLimit { get; set; }
    public int? PauseCyclesLimit { get; set; }

    public TariffBookingDueDateStrategy BookingDueDateStrategy { get; set; }
    public int? BookingDueDateDayOfMonth { get; set; }

    /// <summary>Calculated total at sign-up (plan price + sign-up fee).</summary>
    public decimal TotalSignUpPrice { get; set; }
    /// <summary>Calculated total recurring price per billing cycle.</summary>
    public decimal TotalPrice { get; set; }

    public bool IsVirtualOffice { get; set; }

    public bool WaitForIdentityChecksToActivate { get; set; }
    public bool RequestAddressIdentityCheck { get; set; }
    public string? AddressIdentityCheckDescription { get; set; }
    /// <summary>1 = Manual, 2 = StripeIdentity (<c>eIdentityCheckProvider</c>).</summary>
    public int AddressIdentityCheckProvider { get; set; }
    public bool KeepPausedIfAddressMismatch { get; set; }
    /// <summary>1 = Never, 2 = Every3Months, 3 = Every6Months, 4 = Every12Months, 5 = Every24Months.</summary>
    public int AddressIdentityCheckRepeatPattern { get; set; }
    public bool RequestIdentityCheck { get; set; }
    /// <summary>1 = Manual, 2 = StripeIdentity (<c>eIdentityCheckProvider</c>).</summary>
    public int IdentityCheckProvider { get; set; }
    /// <summary>1 = Never, 2 = Every3Months, 3 = Every6Months, 4 = Every12Months, 5 = Every24Months.</summary>
    public int IdentityCheckRepeatPattern { get; set; }
    public string? IdentityCheckDescription { get; set; }

    public bool RequestAmlCheck { get; set; }
    public bool AmlCheckOpenSanctionsEnabled { get; set; }
    public bool AmlCheckPappersEnabled { get; set; }
    public string? AmlCheckOpenSanctionsDataset { get; set; }
    /// <summary>AML match threshold between 0 and 1 (default 0.7).</summary>
    public decimal? AmlCheckScoreThreshold { get; set; }

    public bool SendOnBoardingFormByEmail { get; set; }
    public int? FormPageId { get; set; }
    public string? FormPageName { get; set; }

    public List<long>? ProductsStore { get; set; }
    public List<long>? ProductsForward { get; set; }
    public List<long>? ProductsRecycle { get; set; }
    public List<long>? ProductsShred { get; set; }
    public List<long>? ProductsScan { get; set; }
    public List<long>? ProductsReturn { get; set; }
    public List<long>? ProductsDeposit { get; set; }
    public List<long>? ProductsCollect { get; set; }

    public int DeliveryPreferencesMail { get; set; }
    public int DeliveryPreferencesParcels { get; set; }
    public int DeliveryPreferencesChecks { get; set; }
    public int DeliveryPreferencesPublicity { get; set; }
    public int DeliveryPreferencesOther { get; set; }

    public int? MaximumDeliveryStorageDays { get; set; }
    public int? MaximumCompanyAliases { get; set; }
    public int? MaximumRecipients { get; set; }
    public int? MaximumAddresses { get; set; }
    public bool TransferProductsToContract { get; set; }
}

/// <summary>Category of a <see cref="Tariff"/> / plan (<c>eTariffType</c>).</summary>
public enum TariffType
{
    None = 0,
    FullTimePrivateOffice = 1,
    PartTimePrivateOffice = 2,
    FullTimeDedicatedDesk = 3,
    PartTimeDedicatedDesk = 4,
    FullTimeHotDesk = 5,
    PartTimeHotDesk = 6,
    FullTimeOther = 7,
    PartTimeOther = 8,
    Storage = 9,
    VirtualOffice = 10,
    Virtual = 11,
    Other = 99
}

/// <summary>When booking charges on a plan become due (<c>eTariffBookingDueDateStrategy</c>).</summary>
public enum TariffBookingDueDateStrategy
{
    None = 0,
    RenewalDate = 1,
    BookingEndDate = 2,
    BookingCreationDate = 3,
    NextNthOfMonth = 4
}
