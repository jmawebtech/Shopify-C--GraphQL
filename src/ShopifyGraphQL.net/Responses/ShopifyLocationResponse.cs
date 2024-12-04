namespace ShopifyGraphQL.Responses
{
    public class ShopifyLocationResponse : ApiResponse
    {
        private readonly List<Location> _item = new List<Location>();

        [JsonProperty("locations")]
        public List<Location> Items
        {
            get { return _item; }
        }
    }
}
