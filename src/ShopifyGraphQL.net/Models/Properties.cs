namespace ShopifyGraphQL.Models
{
    public class Properties
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Value")]
        public object Value { get; set; }
    }
}
