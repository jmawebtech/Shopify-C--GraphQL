namespace ShopifyGraphQL.Models
{
    public class CardDetails
    {
        public string Token { get; set; }
        public string Last4 { get; set; }
        [JsonProperty("expiration_month")]
        public string ExpirationMonth { get; set; }
        [JsonProperty("expiration_year")]
        public string ExpirationYear { get; set; }
        [JsonProperty("card_type")]
        public string CardType { get; set; }
        [JsonProperty("finger_print")]
        public string Fingerprint { get; set; }
    }
}
