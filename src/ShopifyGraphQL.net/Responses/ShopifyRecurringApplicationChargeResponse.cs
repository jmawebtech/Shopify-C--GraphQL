namespace ShopifyGraphQL.Responses
{
    public class ShopifyRecurringApplicationChargeResponse : ApiResponse
    {
        private readonly ShopifyRecurringApplicationCharge _item = new ShopifyRecurringApplicationCharge();
        private readonly List<ShopifyRecurringApplicationCharge> _items = new List<ShopifyRecurringApplicationCharge>();

        [JsonProperty("recurring_application_charge")]
        public ShopifyRecurringApplicationCharge Item
        {
            get { return _item; }
        }

        [JsonProperty("recurring_application_charges")]
        public List<ShopifyRecurringApplicationCharge> Items
        {
            get { return _items; }
        }
    }
}
