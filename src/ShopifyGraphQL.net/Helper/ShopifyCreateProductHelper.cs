namespace ShopifyGraphQL.Helper
{
    /// <summary>
    /// Updates stock, adds to a collection, and adds cost.
    /// </summary>
    public class ShopifyCreateProductHelper
    {
        ShopifyProvider shopifyProvider;
        string locationId;

        public ShopifyCreateProductHelper(ShopifyProvider shopifyProvider, string locationId)
        {
            this.shopifyProvider = shopifyProvider;
            this.locationId = locationId;
        }

        /// <summary>
        /// Creates product without colors or sizes in Shopify and adds stock to its location.
        /// Returns either the product created or an error message.
        /// Adding a stock level exists in another table, since stock is held by inventory site.
        /// Adding purchase cost and enabling inventory tracking is held inside another table.
        /// </summary>
        /// <param name="sku">SKU of product to add stock</param>
        /// <param name="stockQuantity">Stock to update</param>
        /// <param name="stockQuantity">Stock to update</param>
        /// <param name="productCost">Cost to purchase product</param>
        /// <param name="productToCreate">Product to create in Shopify</param>
        /// <returns></returns>
        public async Task<ShopifyProductResponse> CreateProductInShopifyAsync(string sku, decimal stockQuantity, decimal productCost, string collectionName, Product productToCreate)
        {
            ShopifyProductResponse productResponse = await shopifyProvider.ShopifyProductGraphQLConnection.CreateProductAsync(productToCreate);

            if (string.IsNullOrEmpty(productResponse.Product.Id))
            {
                return productResponse;
            }

            // string productId = productResponse.Product.Variants.Where(a => !string.IsNullOrEmpty(a.Sku) && a.Sku.Trim().ToLower() == sku.Trim().ToLower()).FirstOrDefault();

            string productId = productResponse.Product.Variants.Where(a => !string.IsNullOrEmpty(a.Sku) && a.Sku.Trim().ToLower() == sku.Trim().ToLower()).FirstOrDefault()?.InventoryItemId; ;

        
             if (String.IsNullOrEmpty(productId))
            {
                return productResponse;
            }

            await UpdateStockAndAddCostAsync(sku, stockQuantity, productCost, productToCreate, productId);

            //shopifyProvider.Product.ProductList.AddRange(productResponse.Product.Variants);

            return productResponse;
        }

        /// <summary>
        /// Creates product without colors or sizes in Shopify and adds stock to its location.
        /// Returns either the product created or an error message.
        /// Adding a stock level exists in another table, since stock is held by inventory site.
        /// Adding purchase cost and enabling inventory tracking is held inside another table.
        /// </summary>
        /// <param name="sku">SKU of product to add stock</param>
        /// <param name="stockQuantity">Stock to update</param>
        /// <param name="stockQuantity">Stock to update</param>
        /// <param name="productCost">Cost to purchase product</param>
        /// <param name="productToCreate">Product to create in Shopify</param>
        /// <returns></returns>
        public async Task<ShopifyProductResponse> CreateProductVariantInShopifyAsync(string sku, decimal stockQuantity, decimal productCost, string collectionName, Product productToCreate)
        {
            OrganizeVariantsBySize(productToCreate);

            ShopifyProductResponse productResponse = await CreateProductVariantInShopifyAsync(sku, productToCreate);

            if (string.IsNullOrEmpty(productResponse.Variant.Id))
            {
                return productResponse;
            }

            string productId = productResponse.Variant.InventoryItemId;

            await UpdateStockAndAddCostAsync(sku, stockQuantity, productCost, productToCreate, productId);

            //shopifyProvider.Product.ProductList.Add(productResponse.Variant);

            return productResponse;

        }

        private async Task UpdateStockAndAddCostAsync(string sku, decimal stockQuantity, decimal productCost, Product productToCreate, string productId)
        {
            Variant variantToCreate = productToCreate.Variants.Where(a => !string.IsNullOrEmpty(a.Sku) && a.Sku.Trim().ToLower() == sku.Trim().ToLower()).FirstOrDefault();

            if (variantToCreate.RequiresShipping)
            {
                //await shopifyProvider.ShopifyInventoryLevelClient.UpdateStockWithInventoryItemIdAsync(productId, locationId, stockQuantity);
                await shopifyProvider.ShopifyInventoryLevelGraphQLConnection.UpdateStockWithInventoryItemIdAsync(productId, locationId, stockQuantity);

                var shopifyInventoryItem = new ShopifyInventoryItem
                {
                    Id = productId,
                    Sku = sku,
                    Cost = productCost,
                    Tracked = true
                };

                await shopifyProvider.ShopifyInventoryItemGraphQLConnection.AddTrackingAndCostAsync(productId, shopifyInventoryItem);
            }
        }

        /// <summary>
        /// Creates product without colors or sizes in Shopify and adds stock to its location.
        /// Returns either the product created or an error message.
        /// Adding purchase cost and enabling inventory tracking is held inside another table.
        /// </summary>
        /// <param name="sku">SKU of product to add stock</param>
        /// <param name="collectionName">If specified, adds product to a Shopify collection</param>
        /// <param name="productToCreate">Product to create in Shopify</param>
        /// <returns></returns>
        private async Task<ShopifyProductResponse> CreateProductVariantInShopifyAsync(string sku, Product productToCreate)
        {
            Variant variantToCreate = productToCreate.Variants.Where(a => !string.IsNullOrEmpty(a.Sku) && a.Sku.Trim().ToLower() == sku.Trim().ToLower()).FirstOrDefault();
            return await shopifyProvider.ShopifyProductGraphQLConnection.CreateProductVariantAsync(productToCreate, variantToCreate);
        }

        /// <summary>
        /// Ensures small comes before medium and large.
        /// </summary>
        /// <param name="product"></param>
        private void OrganizeVariantsBySize(Product product)
        {
            if (product.Options == null)
                return;

            List<Option> options = product.Options.Where(a => a.Name.ToLower() == "size").ToList();

            if (!options.Any())
                return;
            foreach (Variant variant in product.Variants)
            {
                variant.Position = GetPositionForSize(variant.Option1);
            }

            product.Variants = product.Variants.OrderBy(a => a.Position).ToList();
        }

        private int GetPositionForSize(string size)
        {
            int position = 0;
            switch (size)
            {
                case "Medium":
                case "M":
                    position = 1;
                    break;
                case "Large":
                case "L":
                    position = 2;
                    break;
                case "Extra Large":
                case "XL":
                    position = 3;
                    break;
            }

            return position;
        }
    }
}
