using ShopifyGraphQL;

namespace Examples
{
    internal class Program
    {
        static async Task MainAsync(string[] args)
        {
            string accessToken = "";
            string shopName = "";
            ShopifyProvider shopifyProvider = new ShopifyProvider(accessToken, shopName);
            var data = await shopifyProvider.ShopifyShopGraphQLConnection.SearchAsync();

            Console.WriteLine(data.Item.Name);
        }
    }
}