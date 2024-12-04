namespace ShopifyGraphQL.Queries
{
    /// <summary>
    /// Creates search parameters, like ID numbers or a date range.
    /// </summary>
    public abstract class ShopifyQuery
    {
        public ShopifyQuery()
        {
            ResultsPerPage = ShopifyConstants.ResultsPerPage;
        }

        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
        public DateTime? UpdatedAtMin { get; set; }
        public DateTime? UpdatedAtMax { get; set; }
        public string Id { get; set; }
        public string TimeZone { get; set; }
        /// <summary>
        /// The number of orders to return in each request (page)
        /// </summary>
        public int? ResultsPerPage { get; set; }
        public string Fields { get; set; }

        public virtual IDictionary<string, string> ToDictionary()
        {
            var options = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(Id))
                options.Add("id", Id);

            if (!string.IsNullOrEmpty(Fields))
                options.Add("fields", Fields);

            if (ResultsPerPage.HasValue)
                options.Add("limit", ResultsPerPage.ToString()!);

            ShopifyDateFormatHelper dateFormatHelper = new ShopifyDateFormatHelper();

            if (CreatedAtMin.HasValue)
            {
                string createdMin = dateFormatHelper.MakeISO8601Date(CreatedAtMin.Value, null!);
                string createdMax = dateFormatHelper.MakeISO8601Date(CreatedAtMax!.Value, null!);

                options.Add("created_at_min", createdMin);
                options.Add("created_at_max", createdMax);
            }

            if (UpdatedAtMin.HasValue)
            {
                string updatedMin = dateFormatHelper.MakeISO8601Date(UpdatedAtMin.Value, null);
                string updateMax = dateFormatHelper.MakeISO8601Date(UpdatedAtMax!.Value, null!);

                options.Add("updated_at_min", updatedMin);
                options.Add("updated_at_max", updateMax);
            }

            return options;
        }
    }
}