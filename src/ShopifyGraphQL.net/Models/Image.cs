namespace ShopifyGraphQL.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Src { get; set; }
        public object[] VariantIds { get; set; }
    }
}