namespace ShopifyGraphQL.Models
{
    public class ShopifyPriceSet
    {
        [JsonProperty("presentment_money")]
        public ShopifyMoney PresentmentMoney { get; set; }
        [JsonProperty("shop_money")]
        public ShopifyMoney ShopMoney { get; set; }
    }
}
