namespace ShopifyGraphQL
{
    public class HttpResponse
    {
        public string Body { get; set; }
        public int Status { get; set; }
        public override string ToString()
        {
            return Body;
        }
    }
}
