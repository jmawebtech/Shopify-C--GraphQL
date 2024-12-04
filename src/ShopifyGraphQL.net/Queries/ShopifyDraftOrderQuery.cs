namespace ShopifyGraphQL.Queries
{
    /// <summary>
    /// Creates search parameters, like ID numbers or a date range.
    /// </summary>
    public class ShopifyDraftOrderQuery
    {
        public ShopifyDraftOrderQuery()
        {
            ResultsPerPage = ShopifyConstants.ResultsPerPage;
        }

        public ShopifyOrderStatus? Status { get; set; }
        public DateTime? UpdatedAtMin { get; set; }
        public DateTime? UpdatedAtMax { get; set; }
        /// <summary>
        /// The number of orders to return in each request (page)
        /// </summary>
        public int? ResultsPerPage { get; set; }

        public virtual IDictionary<string, string> ToDictionary()
        {
            var options = new Dictionary<string, string>();

            ShopifyDateFormatHelper dateFormatHelper = new ShopifyDateFormatHelper();

            if (UpdatedAtMin.HasValue)
            {
                string updatedMin = dateFormatHelper.MakeISO8601Date(UpdatedAtMin.Value, null);
                string updateMax = dateFormatHelper.MakeISO8601Date(UpdatedAtMax.Value, null);

                options.Add("updated_at_min", updatedMin);
                options.Add("updated_at_max", updateMax);
            }

            if (Status.HasValue)
            {
                string value = FormatStatus();
                options.Add("status", value);
            }

            return options;
        }

        /// <summary>
        /// Allows the user to import draft and open open orders at the same time.
        /// </summary>
        /// <returns></returns>
        private string FormatStatus()
        {
            string value = EnumHelper.GetString(Status.GetType(), Status.ToString());
            value = value.Replace("draft_", string.Empty);
            return value;
        }
    }
}
