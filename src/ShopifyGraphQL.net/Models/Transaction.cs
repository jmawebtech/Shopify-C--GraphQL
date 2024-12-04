namespace ShopifyGraphQL.Models
{
    public class Transaction
    {
        public Transaction()
        {
            PaymentDetails = new PaymentDetails();
            Receipt = new Receipt();
        }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }
        [JsonProperty("authorization")]
        public string Authorization { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("gateway")]
        public string Gateway { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("kind")]
        public string Kind { get; set; }
        [JsonProperty("location_id")]
        public object LocationId { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        [JsonProperty("parent_id")]
        public string ParentId { get; set; }
        [JsonProperty("source_name")]
        public string SourceName { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("test")]
        public string Test { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
        [JsonProperty("payment_details")]
        public PaymentDetails PaymentDetails { get; set; }
        [JsonProperty("receipt")]
        public Receipt Receipt { get; set; }
    }
}
