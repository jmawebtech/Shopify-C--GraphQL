namespace ShopifyGraphQL.Models
{
    public class ShopifyRecurringApplicationCharge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ApiClientId { get; set; }
        public decimal Price { get; set; }
        public ShopifyChargeStatus Status { get; set; }
        public string ReturnURL { get; set; }
        public object BillingOn { get; set; }
        public int BillingInterval { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Test { get; set; }
        public object ActivatedOn { get; set; }
        public object TrialEndsOn { get; set; }
        public object CancelledOn { get; set; }
        public int TrialDays { get; set; }
        public string DecoratedReturnURL { get; set; }
        public string ConfirmationURL { get; set; }
        public string ErrorMessage { get; set; }
        public string Interval { get; set; }
    }
}
