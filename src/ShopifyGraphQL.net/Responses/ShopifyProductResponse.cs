namespace ShopifyGraphQL.Responses
{
    public class ShopifyProductResponse : ApiResponse
    {
        private readonly List<Product> _products = new List<Product>();
        private readonly Product _product = new Product();
        private readonly Variant _variant = new Variant();
        private List<Variant> _variants = new List<Variant>();

        private int _count = 0;

        [JsonProperty("variants")]
        public List<Variant> Variants
        {
            get { return _variants; }
            set { _variants = value; }
        }

        [JsonProperty]
        public Variant Variant
        {
            get { return _variant; }
        }

        [JsonProperty("count")]
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        [JsonProperty("products")]
        public List<Product> Products
        {
            get { return _products; }
        }

        [JsonProperty("product")]
        public Product Product
        {
            get { return _product; }
        }

    }
}
