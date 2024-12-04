namespace ShopifyGraphQL.Models
{
    public class NoteAttribute
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
