namespace ShopifyGraphQL.Models
{
    public class ShopifyCustomCollection
    {
        public string Id { get; set; }
        public string Handle { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string BodyHtml { get; set; }
        public DateTime PublishedAt { get; set; }
        public string SortOrder { get; set; }
        public string TemplateSuffix { get; set; }
        public string PublishedScope { get; set; }
        public string AdminGraphqlApiId { get; set; }
    }
}
