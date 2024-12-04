namespace ShopifyGraphQL.Models
{
    public class Charges
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }
    }
}