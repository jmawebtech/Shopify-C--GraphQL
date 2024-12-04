namespace ShopifyGraphQL.Responses
{
    public class ShopifyApplicationChargeResponse : ApiResponse
    {
        private readonly ShopifyApplicationCharge _item = new ShopifyApplicationCharge();
        private readonly List<ShopifyApplicationCharge> _items = new List<ShopifyApplicationCharge>();

        [JsonProperty("application_charge")]
        public ShopifyApplicationCharge Item
        {
            get { return _item; }
        }

        [JsonProperty("application_charges")]
        public List<ShopifyApplicationCharge> Items
        {
            get { return _items; }
        }
    }
}
