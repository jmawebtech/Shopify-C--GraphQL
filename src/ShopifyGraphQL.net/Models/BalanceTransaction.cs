namespace ShopifyGraphQL.Models
{
    public class ShopifyBalanceTransaction
    {
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }
    }
}