namespace ShopifyGraphQL.Models
{
    public class AppliedDiscount
    {
        [JsonProperty("value_type")]
        public string ValueType { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
