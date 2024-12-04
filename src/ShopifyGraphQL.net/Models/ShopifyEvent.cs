namespace ShopifyGraphQL.Models
{
    public class ShopifyEvent
    {
        public ShopifyEvent()
        {
            Arguments = new List<string>();
        }

        public string Id { get; set; }
        public string SubjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public SubjectTypeEnum SubjectType { get; set; }
        public VerbEnum Verb { get; set; }
        public List<string> Arguments { get; set; }
        public string Body { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    }
}
