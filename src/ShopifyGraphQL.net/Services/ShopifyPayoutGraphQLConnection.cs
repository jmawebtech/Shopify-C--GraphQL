namespace ShopifyGraphQL.Services
{
    public class ShopifyPayoutGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyPayoutGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        public Task<ShopifyPayoutResponse> SearchAsync(ShopifyPayoutQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
