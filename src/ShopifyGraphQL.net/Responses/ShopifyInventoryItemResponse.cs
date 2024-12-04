namespace ShopifyGraphQL.Responses
{
    public class ShopifyInventoryItemResponse : ApiResponse
    {
        private readonly ShopifyInventoryItem _item = new ShopifyInventoryItem();

        [JsonProperty("inventory_item")]
        public ShopifyInventoryItem Item
        {
            get { return _item; }
        }
    }
}
