namespace ShopifyGraphQL.Responses
{
    public class ShopifyOrderResponse : ApiResponse
    {
        private Order _item = new Order();
        private List<Order> _items = new List<Order>();
        private bool _hasNextPage = false;
        private string _nextPageCursor = string.Empty;

        [JsonProperty("order")]
        public Order Item
        {
            get { return _item; }
            set { _item = value; }
        }

        [JsonProperty("orders")]
        public List<Order> Items
        {
            get { return _items; }
            set { _items = value; }
        }
        // Property to indicate if there is a next page
        [JsonProperty("hasNextPage")]
        public bool HasNextPage
        {
            get { return _hasNextPage; }
            set { _hasNextPage = value; }
        }

        // Property to store the cursor for the next page
        [JsonProperty("nextPageCursor")]
        public string NextPageCursor
        {
            get { return _nextPageCursor; }
            set { _nextPageCursor = value; }
        }
    }
}
