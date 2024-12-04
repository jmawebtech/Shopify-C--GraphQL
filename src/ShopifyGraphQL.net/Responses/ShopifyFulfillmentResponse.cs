namespace ShopifyGraphQL.Responses
{
    public class ShopifyFulfillmentResponse : ApiResponse
    {
        private readonly Fulfillment _item = new Fulfillment();

        [JsonProperty("fulfillment")]
        public Fulfillment Item
        {
            get { return _item; }
        }
    }
}
