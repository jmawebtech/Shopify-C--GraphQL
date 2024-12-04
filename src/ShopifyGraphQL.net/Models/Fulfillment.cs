namespace ShopifyGraphQL.Models
{
    public class Fulfillment
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("tracking_number")]
        public string TrackingNumber { get; set; }
        [JsonProperty("tracking_company")]
        public string TrackingCompany { get; set; }
        [JsonProperty("service")]
        public string Service { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }
        [JsonProperty("location_id")]
        public string LocationId { get; set; }
        [JsonProperty("notify_customer")]
        public bool NotifyCustomer { get; set; }
    }
}
