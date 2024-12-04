namespace ShopifyGraphQL.Models
{
    public class ShopifyMoney
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
