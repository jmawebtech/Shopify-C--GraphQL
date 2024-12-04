namespace ShopifyGraphQL.Queries
{
    public class ShopifyProductByIdsQuery : ShopifyQuery
    {
        /// <summary>
        /// Shopify allows no developer to filter by SKU. Items are pulled and filtered later.
        /// </summary>
        public string Ids { get; set; }


        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> options = base.ToDictionary().ToDictionary(x => x.Key, x => x.Value);

            if (!String.IsNullOrEmpty(Ids))
                options.Add("ids", Ids);

            return options;
        }
    }
}
