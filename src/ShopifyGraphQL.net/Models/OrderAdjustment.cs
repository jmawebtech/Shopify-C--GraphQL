namespace ShopifyGraphQL.Models
{
    public class OrderAdjustment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        [JsonProperty("refund_id")]
        public string RefundId { get; set; }
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }
        [JsonProperty("tax_amount")]
        public decimal? TaxAmount { get; set; }
        [JsonProperty("kind")]
        public ShopifyOrderAdjustmentKind Kind { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
