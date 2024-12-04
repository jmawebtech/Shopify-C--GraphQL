using GraphQL.Query;

namespace ShopifyGraphQL.Queries
{
    public class ShopifyPayoutTransactionQuery : ShopifyAbstractQuery
    {
        public string PayoutId { get; set; }
        public string Limit { get; set; }

        public override IDictionary<string, string> ToDictionary()
        {
            var options = base.ToDictionary();

            if (!String.IsNullOrEmpty(PayoutId))
                options.Add("payout_id", PayoutId);

            if(String.IsNullOrEmpty(Limit))
                options.Add("limit", ShopifyConstants.ResultsPerPage.ToString());

            return options;
        }
    }
}
