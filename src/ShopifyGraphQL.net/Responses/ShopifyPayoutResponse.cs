namespace ShopifyGraphQL.Responses
{
    public class ShopifyPayoutResponse : ApiResponse
    {
        [JsonProperty("payouts")]
        public List<Payout> Payouts { get; } = new List<Payout>();
    }
}
