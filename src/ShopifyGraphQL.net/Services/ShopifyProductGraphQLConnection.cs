using System.Runtime;

namespace ShopifyGraphQL.Services
{
    public class ShopifyProductGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyProductGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        public async Task<ShopifyProductResponse> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<ShopifyProductResponse> CreateProductVariantAsync(Product product, Variant variantToCreate)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(DateTime startDate)
        {
            var query = @"{
                           productsCount(query: ""created_at:>'STARTDATE'"") {
                               count
                               precision
                           }
                        }".Replace("STARTDATE", startDate.ToString());

            var response = await _client.SendQueryAsync<dynamic>(query);

            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ", response.Errors.Select(e => e.Message)));
            }

            var productsCount = response.Data?.productsCount?.count;

            if (int.TryParse(productsCount?.ToString(), out int count))
            {
                return count;
            }

            return 0;
        }

        public async Task<List<Variant>> SearchVariantsBySku(string sku)
        {
            List<Variant> variants = new List<Variant>();
            var query = "query\r\n{ productVariants(first: 1, query: \"sku:" + sku + "\") {\r\n    edges {\r\n      node {\r\n        title\r\n        id\r\n        sku\r\n        product {\r\n          title\r\n        }\r\n        inventoryItem {\r\n          id\r\n          inventoryLevels(first: 3) {\r\n            edges {\r\n              node {\r\n                id\r\n                location {\r\n                  id\r\n                  name\r\n                  address {\r\n                    city\r\n                  }\r\n                }\r\n              }\r\n            }\r\n          }\r\n        }\r\n      }\r\n    }\r\n  }\r\n}";

            var response = await _client.SendQueryAsync<dynamic>(query);

            foreach (var edge in response.Data.productVariants.edges)
            {
                var node = edge.node;
                Variant variant = new Variant
                {
                    Title = node.title,
                    Id = node.id.ToString().Replace("gid://shopify/ProductVariant/", ""),
                    Sku = node.sku,
                    ProductTitle = node.product.title,
                    InventoryItemId = node.inventoryItem.id.ToString().Replace("gid://shopify/InventoryItem/", ""),
                };

                foreach (var inventoryEdge in node.inventoryItem.inventoryLevels.edges)
                {
                    var inventoryNode = inventoryEdge.node;
                    InventoryLevel inventoryLevel = new InventoryLevel
                    {
                        LocationName = inventoryNode.location.name,
                        City = inventoryNode.location.address.city
                    };
                    variant.InventoryLevels.Add(inventoryLevel);
                }

                variants.Add(variant);
            }

            return variants;
        }
        public async Task<ShopifyProductResponse> UpdateProductPriceAsync(string sku, decimal price)
        {
            var productResponse = new ShopifyProductResponse();

            if (string.IsNullOrEmpty(sku))
            {
                productResponse.HttpResponse = new HttpResponse
                {
                    Body = $"There is no product with a SKU of {sku} in Shopify. Please create it before syncing inventory and pricing updates."
                };
                return productResponse;
            }
            var variants = await SearchVariantsAsync(sku);

            if (!variants.Any())
            {
                productResponse.HttpResponse = new HttpResponse
                {
                    Body = $"There is no product with a SKU of {sku} in Shopify. Please create it before syncing inventory and pricing updates."
                };
                return productResponse;
            }

            const string mutation = @"
            mutation productVariantsBulkUpdate($productId: ID!, $variants: [ProductVariantsBulkInput!]!) {
                productVariantsBulkUpdate(productId: $productId, variants: $variants) {
                    product {
                        id
                    }
                    productVariants {
                        id
                        price
                    }
                    userErrors {
                        field
                        message
                    }
                }
            }
            ";
            var productId = variants.FirstOrDefault()?.ProductId;
            var variantInputs = variants.Select(variant => new
            {
                id = $"gid://shopify/ProductVariant/{variant.Id}",
                price = price,
            }).ToList();

            var variables = new
            {
                productId = $"gid://shopify/Product/{productId}",
                variants = variantInputs
            };
            var graphQLResponse = await _client.SendMutationAsync<dynamic>(new GraphQLRequest
            {
                Query = mutation,
                Variables = variables
            });

            if (graphQLResponse.Errors != null && graphQLResponse.Errors.Any())
            {
                productResponse.HttpResponse = new HttpResponse
                {
                    Body = string.Join(", ", graphQLResponse.Errors.Select(e => e.Message))
                };
                return productResponse;
            }

            var responseData = graphQLResponse.Data?.productVariantsBulkUpdate;
            if (responseData == null)
            {
                productResponse.HttpResponse = new HttpResponse
                {
                    Body = "Failed to update product metafields. No data returned."
                };
                return productResponse;
            }

            if (responseData.userErrors is IEnumerable<dynamic> userErrors && userErrors.Any())
            {
                productResponse.HttpResponse = new HttpResponse
                {
                    Body = string.Join(", ", userErrors.Select(e => (string)e.message))
                };
                return productResponse;
            }
            if (responseData.productVariants != null)
            {
                productResponse.Variants = ((IEnumerable<dynamic>)responseData.productVariants)
                    .Select(v => new Variant
                    {
                        Id = v.id,
                        Price = v.price,
                    }).ToList();
            }

            productResponse.HttpResponse = new HttpResponse
            {
                Status = 200,
                Body = "Product and variant metafields updated successfully."
            };

            return productResponse;
        }

        public async Task<List<Variant>> SearchVariantsAsync(string sku)
        {
            var variants = new List<Variant>();
            string query;
            object variables;

            if (!string.IsNullOrEmpty(sku))
            {
                query = @"
        query($sku: String!) {
            productVariants(first: 1, query: $sku) {
                edges {
                    node {
                        id
                        sku
                        price
                        compareAtPrice
                        createdAt
                        updatedAt
                        inventoryItem {
                            id
                        }
                        product {
                            id
                            title
                        }
                        inventoryManagement
                        inventoryPolicy
                        position
                        requiresShipping
                        taxable
                        title
                        barcode
                        image {
                            id
                        }
                    }
                }
                pageInfo {
                    hasNextPage
                    endCursor
                }
            }
        }";
                variables = new { sku = $"sku:{sku}" };
            }
            else
            {
                query = @"
        query($first: Int, $after: String) {
            productVariants(first: $first, after: $after) {
                edges {
                    node {
                        id
                        sku
                        price
                        compareAtPrice
                        createdAt
                        updatedAt
                        inventoryItem {
                            id
                        }
                        product {
                            id
                            title
                        }
                        inventoryManagement
                        position
                        requiresShipping
                        taxable
                        title
                        barcode
                        image {
                            id
                        }
                    }
                }
                pageInfo {
                    hasNextPage
                    endCursor
                }
            }
        }";
                variables = new { first = 250, after = (string)null };
            }

            string cursor = null;

            do
            {
                if (cursor != null)
                {
                    variables = new { first = 250, after = cursor };
                }

                var graphQLResponse = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
                {
                    Query = query,
                    Variables = variables
                });

                if (graphQLResponse.Errors != null && graphQLResponse.Errors.Any())
                {
                    throw new Exception($"GraphQL error: {string.Join(", ", graphQLResponse.Errors.Select(e => e.Message))}");
                }

                var data = graphQLResponse.Data?.productVariants;
                if (data == null)
                {
                    throw new Exception("No data returned from Shopify.");
                }

                DateTime createdAt;
                DateTime updatedAt;
                foreach (var edge in data.edges)
                {
                    var node = edge.node as JObject;

                    if (node == null)
                        continue;

                    variants.Add(new Variant
                    {
                        Id = node["id"]?.ToString().Replace("gid://shopify/ProductVariant/", ""),
                        Sku = node["sku"]?.ToString(),
                        Price = node["price"]?.ToString(),
                        CompareAtPrice = node["compareAtPrice"]?.ToString(),
                        CreatedAt = DateTime.TryParse(node["createdAt"]?.ToString(), out createdAt) ? createdAt : DateTime.MinValue,
                        UpdatedAt = DateTime.TryParse(node["updatedAt"]?.ToString(), out updatedAt) ? updatedAt : DateTime.MinValue,
                        InventoryItemId = node["inventoryItem"]?["id"]?.ToString()?.Replace("gid://shopify/InventoryItem/", ""),
                        ProductId = node["product"]?["id"]?.ToString()?.Replace("gid://shopify/Product/", ""),
                        ProductTitle = node["product"]?["title"]?.ToString(),
                        InventoryManagement = node["inventoryManagement"]?.ToString(),
                        Position = node["position"] != null ? Convert.ToInt32(node["position"]) : 0,
                        RequiresShipping = node["requiresShipping"] != null && Convert.ToBoolean(node["requiresShipping"]),
                        Taxable = node["taxable"] != null && Convert.ToBoolean(node["taxable"]),
                        Title = node["title"]?.ToString(),
                        Barcode = node["barcode"]?.ToString(),
                    });
                }

                cursor = (bool)data.pageInfo.hasNextPage ? (string)data.pageInfo.endCursor : null;

            } while (cursor != null);

            return variants;
        }
        //public async Task<ShopifyProductResponse> UpdateProductPriceBulkAsync(string sku, decimal price)
        //{
        //    var productResponse = new ShopifyProductResponse();

        //    var variants = await SearchVariantsAsync(sku);

        //    if (string.IsNullOrEmpty(sku) || variants == null || !variants.Any())
        //    {
        //        productResponse.HttpResponse = new HttpResponse
        //        {
        //            Body = "Product ID or variants cannot be null or empty."
        //        };
        //        return productResponse;
        //    }
        //    var mutation = @"
        //    mutation productVariantsBulkUpdate($productId: ID!, $variants: [ProductVariantsBulkInput!]!) {
        //      productVariantsBulkUpdate(productId: $productId, variants: $variants) {
        //        product {
        //          id
        //          title
        //          vendor
        //          productType
        //          tags
        //          updatedAt
        //          createdAt
        //          publishedAt
        //          variants(first: 5) {
        //            edges {
        //              node {
        //                id
        //                price
        //              }
        //            }
        //          }
        //        }
        //        productVariants {
        //          id
        //          price
        //          metafields(first: 2) {
        //            edges {
        //              node {
        //                namespace
        //                key
        //                value
        //              }
        //            }
        //          }
        //        }
        //        userErrors {
        //          field
        //          message
        //        }
        //      }
        //    }";

        //    var variables = new
        //    {
        //        productId = $"gid://shopify/Product/{sku}",
        //        variants = variants.Select(v => new
        //        {
        //            id = $"gid://shopify/ProductVariant/{v.Id}",
        //            price = price
        //        }).ToList()
        //    };

        //    var response = await _client.SendMutationAsync<dynamic>(new GraphQLRequest
        //    {
        //        Query = mutation,
        //        Variables = variables
        //    });

        //    if (response.Errors != null && response.Errors.Any())
        //    {
        //        productResponse.HttpResponse = new HttpResponse
        //        {
        //            Body = string.Join(", ", response.Errors.Select(e => e.Message))
        //        };
        //        return productResponse;
        //    }

        //    var responseData = response.Data?.productVariantsBulkUpdate;

        //    if (responseData?.userErrors != null && ((IEnumerable<dynamic>)responseData.userErrors).Any())
        //    {
        //        var userErrors = ((IEnumerable<dynamic>)responseData.userErrors)
        //            .Select(e => (string)e.message)
        //            .ToList();

        //        productResponse.HttpResponse = new HttpResponse
        //        {
        //            Body = string.Join(", ", userErrors)
        //        };
        //        return productResponse;
        //    }


        //    if (responseData?.productVariants != null)
        //    {
        //        foreach (var variant in responseData.productVariants)
        //        {
        //            var newVariant = new Variant
        //            {
        //                Id = variant.id.ToString().Replace("gid://shopify/ProductVariant/", ""),
        //                Price = variant.price,
        //                UpdatedAt = DateTime.UtcNow
        //            };

        //            productResponse.Variants.Add(newVariant);
        //        }
        //    }

        //    if (responseData?.product != null)
        //    {
        //        var product = responseData.product;

        //        var newProduct = new Product
        //        {
        //            Id = product.id.ToString().Replace("gid://shopify/Product/", ""),
        //            Title = product.title,
        //            Vendor = product.vendor,
        //            ProductType = product.productType,
        //            Tags = product.tags,
        //            UpdatedAt = product.updatedAt,
        //            CreatedAt = product.createdAt,
        //            PublishedAt = product.publishedAt,
        //            Variants = (product.variants.edges as IEnumerable<dynamic>)?.Select(edge => new Variant
        //            {
        //                Id = ((string)edge.node.id).Replace("gid://shopify/ProductVariant/", ""),
        //                Price = edge.node.price,
        //                UpdatedAt = DateTime.UtcNow
        //            }).ToList()
        //        };

        //        productResponse.Products.Add(newProduct);
        //    }

        //    productResponse.HttpResponse = new HttpResponse
        //    {
        //        Body = "Product and variants updated successfully."
        //    };

        //    return productResponse;
        //}
    }
}
