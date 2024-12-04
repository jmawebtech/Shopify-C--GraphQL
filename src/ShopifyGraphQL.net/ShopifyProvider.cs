using ShopifyGraphQL.Services;

namespace ShopifyGraphQL
{
    public class ShopifyProvider
    {
        public ShopifyProductGraphQLConnection ShopifyProductGraphQLConnection;
        public ShopifyShopGraphQLConnection ShopifyShopGraphQLConnection;
        public ShopifyInventoryLevelGraphQLConnection ShopifyInventoryLevelGraphQLConnection;
        public ShopifyInventoryItemGraphQLConnection ShopifyInventoryItemGraphQLConnection;
        public ShopifyOrderGraphQLConnection ShopifyOrderGraphQLConnection;

        public ShopifyProvider(string accessToken, string shopName)
        {
            ShopifyProductGraphQLConnection = new ShopifyProductGraphQLConnection(shopName, accessToken);
            ShopifyShopGraphQLConnection = new ShopifyShopGraphQLConnection(shopName, accessToken);
            ShopifyInventoryLevelGraphQLConnection = new ShopifyInventoryLevelGraphQLConnection(shopName, accessToken);
            ShopifyInventoryItemGraphQLConnection = new ShopifyInventoryItemGraphQLConnection(shopName, accessToken);
            ShopifyOrderGraphQLConnection = new ShopifyOrderGraphQLConnection(shopName, accessToken);
        }
    }
}
