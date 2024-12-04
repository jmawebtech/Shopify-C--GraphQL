namespace ShopifyGraphQL.Models
{
    public class ShopifyCollect
    {
        public string Id { get; set; }
        public string CollectionId { get; set; }
        public string ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Position { get; set; }
        public string SortValue { get; set; }
    }
}
