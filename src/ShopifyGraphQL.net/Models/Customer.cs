namespace ShopifyGraphQL.Models
{
    public class Customer
    {
        public Customer()
        {
            DefaultAddress = new Address();
        }

        [JsonProperty("accepts_marketing")]
        public bool AcceptsMarketing { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("last_order_id")]
        public string LastOrderId { get; set; }
        [JsonProperty("multipass_identifier")]
        public string MultipassIdentifier { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("orders_count")]
        public string OrdersCount { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("total_spent")]
        public string TotalSpent { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("verified_email")]
        public bool verified_email { get; set; }
        [JsonProperty("tags")]
        public string Tags { get; set; }
        [JsonProperty("last_order_name")]
        public string LastOrderName { get; set; }
        [JsonProperty("default_address")]
        public Address DefaultAddress { get; set; }
    }
}
