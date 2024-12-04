namespace ShopifyGraphQL.Models
{
    public class Option
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string ProductId { get; set; }
        public List<string> Values { get; set; }
    }
}