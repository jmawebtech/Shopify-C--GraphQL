namespace ShopifyGraphQL.Queries
{
    /// <summary>
    /// Creates search parameters, like ID numbers or a date range.
    /// </summary>
    public class ShopifyProductQuery : ShopifyQuery
    {
        /// <summary>
        /// Shopify allows no developer to filter by SKU. Items are pulled and filtered later.
        /// </summary>
        public string Sku { get; set; }

        public ShopifyProductQuery()
        {
        }

        public string Title { get; set; }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> options = base.ToDictionary().ToDictionary(x => x.Key, x => x.Value);

            if (!String.IsNullOrEmpty(Title))
                options.Add("title", Title);

            return options;
        }
    }
}