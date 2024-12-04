namespace ShopifyGraphQL.Responses
{
    //public class ShopifyShopResponse : ApiResponse
    //{
    //    private readonly Shop _item = new Shop();

    //    [JsonProperty("shop")]
    //    public Shop Item
    //    {
    //        get { return _item; }
    //    }
    //}

    public class ShopifyShopResponse : ApiResponse
    {
        [JsonProperty("shop")]
        public Shop Item { get; set; }
    }
}
