namespace ShopifyGraphQL.Responses
{
    public class ShopifyTransactionResponse : ApiResponse
    {
        private readonly List<Transaction> _items = new List<Transaction>();

        [JsonProperty("transactions")]
        public List<Transaction> Items
        {
            get { return _items; }
        }
    }
}
