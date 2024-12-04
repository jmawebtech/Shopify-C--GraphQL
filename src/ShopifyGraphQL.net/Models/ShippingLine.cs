namespace ShopifyGraphQL.Models
{
    public class ShippingLine
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("price")]
        public decimal? Price { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("discounted_price")]
        public decimal? DiscountedPrice { get; set; }
        [JsonProperty("tax_lines")]
        public List<TaxLine> TaxLines = new List<TaxLine>();
        [JsonProperty("price_set")]
        public ShopifyPriceSet PriceSet { get; set; }
    }
}
