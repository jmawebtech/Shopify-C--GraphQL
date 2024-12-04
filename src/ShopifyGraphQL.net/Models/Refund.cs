namespace ShopifyGraphQL.Models
{
    public class Refund
    {
        public Refund()
        {
            RefundLineItems = new List<RefundLineItem>();
            OrderAdjustments = new List<OrderAdjustment>();
            Transactions = new List<Transaction>();
        }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        [JsonProperty("restock")]
        public bool? Restock { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("refund_line_items")]
        public List<RefundLineItem> RefundLineItems { get; set; }
        [JsonProperty("order_adjustments")]
        public List<OrderAdjustment> OrderAdjustments { get; set; }
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    }
}
