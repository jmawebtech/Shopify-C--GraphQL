namespace ShopifyGraphQL.Responses
{
    public class ShopifyEventResponse : ApiResponse
    {
        private readonly List<ShopifyEvent> _items = new List<ShopifyEvent>();

        [JsonProperty("events")]
        public List<ShopifyEvent> Items
        {
            get { return _items; }
        }
    }
}
