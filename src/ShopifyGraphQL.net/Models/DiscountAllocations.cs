namespace ShopifyGraphQL.Models
{
    public class DiscountAllocations
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("discount_application_index")]
        public int DiscountApplicationIndex { get; set; }
        [JsonProperty("amount_set")]
        public ShopifyPriceSet AmountSet { get; set; }
    }
}
