using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace ShopifyGraphQL.Services
{
    public abstract class ShopifyGraphQLConnection
    {
        protected readonly GraphQLHttpClient _client;

        public ShopifyGraphQLConnection(string shopName, string accessToken)
        {
            string endpoint = $"admin/api/{ShopifyConstants.API_Date}/graphql.json";
            _client = new GraphQLHttpClient($"https://{shopName}.myshopify.com/" + endpoint, new NewtonsoftJsonSerializer());
            _client.HttpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", accessToken);
        }

    }
}
