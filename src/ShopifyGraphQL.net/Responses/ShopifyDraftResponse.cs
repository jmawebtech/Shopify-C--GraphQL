namespace ShopifyGraphQL.Responses
{
    public class ShopifyDraftOrderResponse : ApiResponse
    {
        private Order _item = new Order();
        private List<Order> _items = new List<Order>();

        [JsonProperty("draft_order")]
        public Order Item
        {
            get { return _item; }
            set { _item = value; }
        }

        [JsonProperty("draft_orders")]
        public List<Order> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
