namespace ShopifyGraphQL.Models
{
    public class TaxLine
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("rate")]
        public decimal Rate { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("price_set")]
        public ShopifyPriceSet PriceSet { get; set; }
    }
}
