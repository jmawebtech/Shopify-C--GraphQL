namespace ShopifyGraphQL.Responses
{
    public class ShopifyPayoutTransactionResponse : ApiResponse
    {
        [JsonProperty("transactions")]
        public List<PayoutTransaction> Transactions { get; } = new List<PayoutTransaction>();
    }
}
