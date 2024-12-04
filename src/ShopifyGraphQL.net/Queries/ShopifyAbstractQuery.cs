namespace GraphQL.Query { 
    public abstract class ShopifyAbstractQuery
    {
        public virtual IDictionary<string, string> ToDictionary()
        {
            var options = new Dictionary<string, string>();
            return options;
        }
    }
}