# Shopify C# GraphQL
 
This library works with the new Graph QL APIs for Shopify. Here is what we cover:

1. Orders
2. Products
3. Location
4. Shop

As of 12/4/2024, this library lacks code to create products. I will add more GraphQL queries, as I create them.

If you wish to contribute, please message me.

# About Us

Connex Ecommerce eliminates manual data entry between back office systems, like QuickBooks, and ordering systems like Shopify. We are available to hire for your custom software needs. For more info on integrating with us, please visit our website:

https://connexecommerce.com/custom-integrations

If you wish to use our services, please message me or contact our sales team: 

https://connexecommerce.com/contact-sales

# How to use

Review the examples folder program.cs for examples.

```
            string accessToken = "";
            string shopName = "";
            ShopifyProvider shopifyProvider = new ShopifyProvider(accessToken, shopName);
            var data = await shopifyProvider.ShopifyShopGraphQLConnection.SearchAsync();

```