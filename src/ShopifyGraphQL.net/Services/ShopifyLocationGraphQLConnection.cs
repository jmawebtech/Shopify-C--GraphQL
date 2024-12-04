namespace ShopifyGraphQL.Services
{
    public class ShopifyLocationGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyLocationGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        public Task<ShopifyLocationResponse> SearchAsync()
        {
            throw new NotImplementedException();
        }
    }
}