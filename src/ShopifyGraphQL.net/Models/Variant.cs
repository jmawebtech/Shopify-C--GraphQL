namespace ShopifyGraphQL.Models
{
    public class Variant
    {
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonProperty("compare_at_price", NullValueHandling = NullValueHandling.Include)]
        public object CompareAtPrice { get; set; }
        public string FulfillmentService { get; set; }
        public string Grams { get; set; }
        public string Id { get; set; }
        public string InventoryManagement { get; set; }
        public InventoryPolicy InventoryPolicy { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public int Position { get; set; }
        public string Price { get; set; }
        public string ProductId { get; set; }
        public bool RequiresShipping { get; set; }
        public string Sku { get; set; }
        public bool Taxable { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        public object ImageId { get; set; }
        public string InventoryItemId { get; set; }

   
        public string ProductTitle { get; set; }
        public List<InventoryLevel> InventoryLevels { get; set; } = new();
    }

    public class InventoryLevel
    {
        public string LocationName { get; set; }
        public string City { get; set; }
    }

    public class ResponseType
    {
        public Products Products { get; set; }
    }

    public class Products
    {
        public Edge[] Edges { get; set; }
    }

    public class Edge
    {
        public Node Node { get; set; }
    }

    public class Node
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

}