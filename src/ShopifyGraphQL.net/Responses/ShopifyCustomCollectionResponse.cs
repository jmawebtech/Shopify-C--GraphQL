namespace ShopifyGraphQL.Responses
{
    public class ShopifyCustomCollectionResponse : ApiResponse
    {
        private readonly ShopifyCustomCollection _item = new ShopifyCustomCollection();
        private readonly List<ShopifyCustomCollection> _items = new List<ShopifyCustomCollection>();

        [JsonProperty("custom_collection")]
        public ShopifyCustomCollection Item
        {
            get { return _item; }
        }

        [JsonProperty("custom_collections")]
        public List<ShopifyCustomCollection> Items
        {
            get { return _items; }
        }
    }
}
