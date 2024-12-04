namespace ShopifyGraphQL.Models
{
    public class Shop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Domain { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public object Source { get; set; }
        public string Phone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PrimaryLocale { get; set; }
        public string Address2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Currency { get; set; }
        public string CustomerEmail { get; set; }
        public string Timezone { get; set; }
        public string IanaTimezone { get; set; }
        public string ShopOwner { get; set; }
        public string MoneyFormat { get; set; }
        public string MoneyWithCurrencyFormat { get; set; }
        public string WeightUnit { get; set; }
        public string ProvinceCode { get; set; }
        public bool? TaxesIncluded { get; set; }
        public bool? TaxShipping { get; set; }
        public bool? CountyTaxes { get; set; }
        public string PlanDisplayName { get; set; }
        public string PlanName { get; set; }
        public bool? HasDiscounts { get; set; }
        public bool? HasGiftCards { get; set; }
        public string MyshopifyDomain { get; set; }
        public object GoogleAppsDomain { get; set; }
        public object GoogleAppsLoginEnabled { get; set; }
        public string MoneyInEmailsFormat { get; set; }
        public string MoneyWithCurrencyInEmailsFormat { get; set; }
        public bool? EligibleForPayments { get; set; }
        public bool? RequiresExtraPaymentsAgreement { get; set; }
        public bool? PasswordEnabled { get; set; }
        public bool? HasStorefront { get; set; }
        public object EligibleForCardReaderGiveaway { get; set; }
        public bool? Finances { get; set; }
        public string PrimaryLocationId { get; set; }
        public bool? CheckoutApiSupported { get; set; }
        public bool? MultiLocationEnabled { get; set; }
        public bool? SetupRequired { get; set; }
        public bool? ForceSSL { get; set; }
        public bool? PreLaunchEnabled { get; set; }
    }
}
