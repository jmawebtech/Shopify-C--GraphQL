namespace ShopifyGraphQL.Responses
{
    public class ShopifyInventoryLevelResponse : ApiResponse
    {
        private readonly ShopifyInventoryLevel _item = new ShopifyInventoryLevel();
        private readonly List<ShopifyInventoryLevel> _items = new List<ShopifyInventoryLevel>();

        [JsonProperty("inventory_level")]
        public ShopifyInventoryLevel Item
        {
            get { return _item; }
        }

        [JsonProperty("inventory_levels")]
        public List<ShopifyInventoryLevel> Items
        {
            get { return _items; }
        }
    }
}
