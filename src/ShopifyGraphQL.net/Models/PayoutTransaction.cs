namespace ShopifyGraphQL.Models
{
    public class PayoutTransaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public PayoutTransactionType Type { get; set; }

        [JsonProperty("test")]
        public bool Test { get; set; }

        [JsonProperty("payout_id")]
        public string PayoutId { get; set; }

        [JsonProperty("payout_status")]
        public string PayoutStatus { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("net")]
        public decimal Net { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }

        [JsonProperty("source_type")]
        public string SourceType { get; set; }

        [JsonProperty("source_order_id")]
        public string SourceOrderId { get; set; }

        [JsonProperty("source_order_transaction_id")]
        public string SourceOrderTransactionId { get; set; }

        [JsonProperty("processed_at")]
        public DateTime ProcessedAt { get; set; }
    }
}
