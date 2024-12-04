namespace ShopifyGraphQL.Queries
{
    /// <summary>
    /// Creates search parameters, like ID numbers or a date range.
    /// </summary>
    public class ShopifyOrderQuery : ShopifyQuery
    {
        public ShopifyOrderQuery()
        {
        }

        public string OrderNumber { get; set; }
        public string Title { get; set; }
        public DateTime? ProcessedAtMin { get; set; }
        public DateTime? ProcessedAtMax { get; set; }
        public string OrderStatus { get; set; }

        public override IDictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> options = base.ToDictionary().ToDictionary(x => x.Key, x => x.Value);

            if (!String.IsNullOrEmpty(OrderNumber))
                options.Add("name", OrderNumber.Replace("#", string.Empty));

            if (!String.IsNullOrEmpty(Title))
                options.Add("title", Title);

            if (!String.IsNullOrEmpty(OrderStatus) && IsShippedStatus(OrderStatus))
            {
                options.Add("fulfillment_status", GetFulfillmentStatus());
            }
            else if(!String.IsNullOrEmpty(OrderStatus) && IsGlobalStatus(OrderStatus))
            {
                options.Add("status", OrderStatus);
            }
            else if (!String.IsNullOrEmpty(OrderStatus))
            {
                string financialStatus = EnumHelper.GetString(typeof(FinancialStatus), OrderStatus);
                options.Add("financial_status", financialStatus);
            }

            if(!IsGlobalStatus(OrderStatus))
                options.Add("status", "any");

            if (ProcessedAtMin.HasValue)
            {
                string processedMin = ProcessedAtMin.Value.ToString("yyyy-MM-dd HH':'mm':'ss");
                string processedMax = ProcessedAtMax.Value.ToString("yyyy-MM-dd HH':'mm':'ss");

                options.Add("processed_at_min", processedMin);
                options.Add("processed_at_max", processedMax);
            }


            return options;
        }

        private bool IsGlobalStatus(string statusName)
        {
            string[] statuses = new string[] { "open", "closed" };
            bool isGlobal = !String.IsNullOrEmpty(statusName) && statuses.Contains(statusName.Trim().ToLower());
            return isGlobal;
        }

        private bool IsShippedStatus(string statusName)
        {
            string[] shippedStatuses = new string[] { "fulfilled", "shipped", "partial", "unshipped" };
            bool isShipped = !String.IsNullOrEmpty(statusName) && shippedStatuses.Contains(statusName.Trim().ToLower());
            return isShipped;
        }

        public string GetFulfillmentStatus()
        {
            if (OrderStatus == "fulfilled")
                return "shipped";
            if (OrderStatus == "partial")
                return "partial";
            return EnumHelper.GetString(typeof(FulfillmentStatus), OrderStatus.ToString());
        }
    }
}