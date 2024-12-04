namespace ShopifyGraphQL.Models
{
    public class ShopifyInventoryLevel
    {
        public string InventoryItemId { get; set; }
        public string LocationId { get; set; }
        public int Available { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AdminGraphqlApiId { get; set; }
    }
}
