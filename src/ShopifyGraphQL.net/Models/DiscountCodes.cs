namespace ShopifyGraphQL.Models
{
    public class DiscountCodes
    {
        [JsonProperty("Amount")]
        public decimal Amount { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
