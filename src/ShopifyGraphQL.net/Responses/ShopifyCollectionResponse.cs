namespace ShopifyGraphQL.Responses
{
    public class ShopifyCollectionResponse : ApiResponse
    {
        private readonly ShopifyCollect _item = new ShopifyCollect();
        private readonly List<ShopifyCollect> _items = new List<ShopifyCollect>();

        [JsonProperty("collect")]
        public ShopifyCollect Item
        {
            get { return _item; }
        }

        [JsonProperty("collects")]
        public List<ShopifyCollect> Items
        {
            get { return _items; }
        }
    }
}
