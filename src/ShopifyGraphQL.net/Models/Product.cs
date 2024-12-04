namespace ShopifyGraphQL.Models
{
    public class Product
    {
        [JsonProperty("body_html")]
        public string BodyHtml { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("handle")]
        public string Handle { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("product_type")]
        public string ProductType { get; set; }
        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; set; }
        [JsonProperty("published_scope")]
        public string PublishedScope { get; set; }
        [JsonProperty("template_suffix")]
        public object TemplateSuffix { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("vendor")]
        public string Vendor { get; set; }
        [JsonProperty("tags")]
        public string Tags { get; set; }
        [JsonProperty("variants")]
        public List<Variant> Variants { get; set; }
        [JsonProperty("options")]
        public List<Option> Options { get; set; }
        [JsonProperty("Images")]
        public Image[] Images { get; set; }
    }
}