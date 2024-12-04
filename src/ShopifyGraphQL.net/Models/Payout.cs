namespace ShopifyGraphQL.Models
{
    public class Payout
    {
        public Payout()
        {
            Summary = new Summary();
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }
}
