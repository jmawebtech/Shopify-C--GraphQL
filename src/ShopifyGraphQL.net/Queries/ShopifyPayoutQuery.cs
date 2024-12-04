namespace ShopifyGraphQL.Queries
{
    /// <summary>
    /// Creates search parameters, like ID numbers or a date range.
    /// </summary>
    public class ShopifyPayoutQuery
    {
        public ShopifyPayoutQuery()
        {
        }

        public DateTime? Date { get; set; }

        public virtual IDictionary<string, string> ToDictionary()
        {
            var options = new Dictionary<string,string>();

            ShopifyDateFormatHelper dateFormatHelper = new ShopifyDateFormatHelper();

            if (Date.HasValue)
            {
                options.Add("date", Date.Value.ToString("yyyy-MM-dd"));         
            }

            return options;
        }
    }
}