namespace ShopifyGraphQL.Models
{
    public class ShopifyInventoryItem
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal Cost { get; set; }
        public bool Tracked { get; set; }
        public string AdminGraphqlApiId { get; set; }
    }
}
