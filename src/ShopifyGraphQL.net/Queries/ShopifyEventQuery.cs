namespace ShopifyGraphQL.Queries
{
    public class ShopifyEventQuery : ShopifyQuery
    {
        public string Verb { get; set; }
        public string Filter { get; set; }
        public string OrderId { get; set; }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> options = base.ToDictionary().ToDictionary(x => x.Key, x => x.Value);

            if (!String.IsNullOrEmpty(Verb))
                options.Add("verb", Verb);

            if (!String.IsNullOrEmpty(Filter))
                options.Add("filter", Filter);


            return options;
        }
    }
}