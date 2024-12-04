namespace ShopifyGraphQL.Models
{
    public class ShopifyApplicationCharge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ApiClientId { get; set; }
        public decimal Price { get; set; }
        public ShopifyChargeStatus Status { get; set; }
        public string ReturnURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Test { get; set; }
        public object ChargeType { get; set; }
        public string DecoratedReturnURL { get; set; }
        public string ConfirmationURL { get; set; }
        public string ErrorMessage { get; set; }
    }
}
