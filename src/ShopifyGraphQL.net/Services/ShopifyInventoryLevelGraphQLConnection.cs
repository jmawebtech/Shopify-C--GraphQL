using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyGraphQL.Services
{
    public class ShopifyInventoryLevelGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyInventoryLevelGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        public async Task<ShopifyInventoryLevelResponse> UpdateStockWithSkuAsync(
        List<Variant> variants, string locationId, string sku, decimal quantity)
        {
            var response = new ShopifyInventoryLevelResponse();

            if (!variants.Any())
            {
                response.HttpResponse = new HttpResponse
                {
                    Body = $"There is no product with a SKU of {sku} in Shopify. Please create it before syncing inventory.",
                    Status = 400
                };
                return response;
            }

            foreach (var variant in variants)
            {
                if (!int.TryParse(quantity.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out int quantityInt))
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = $"Product {sku} has a stock quantity of {quantity}. Shopify only accepts whole numbers. Please round up or round down your stock.",
                        Status = 400
                    };
                    return response;
                }

                if (string.IsNullOrEmpty(variant.InventoryManagement))
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = $"Product {variant.Sku} has stock management turned off. Log into Shopify. Click products and open the product. Scroll down to inventory. Under inventory policy, select Shopify tracks this product's inventory.",
                        Status = 400
                    };
                    return response;
                }

                string inventoryItemId = $"gid://shopify/InventoryItem/{variant.InventoryItemId}";
                if (string.IsNullOrEmpty(locationId))
                {
                    var locationQuery = @"
        query inventoryItemToProductVariant($inventoryItemId: ID!) {
            inventoryItem(id: $inventoryItemId) {
                id
                inventoryLevels(first: 1) {
                    edges {
                        node {
                            id
                            location {
                                id
                                name
                            }
                        }
                    }
                }
            }
        }";

                    var locationVariables = new
                    {
                        inventoryItemId = inventoryItemId
                    };

                    var locationResponse = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
                    {
                        Query = locationQuery,
                        Variables = locationVariables
                    });

                    // Check if there are any errors
                    if (locationResponse.Errors != null && locationResponse.Errors.Any())
                    {
                        response.HttpResponse = new HttpResponse
                        {
                            Body = string.Join(", ", locationResponse.Errors.Select(e => e.Message)),
                            Status = 400
                        };
                        return response;
                    }

                    if (locationResponse.Data == null || locationResponse.Data.inventoryItem == null)
                    {
                        response.HttpResponse = new HttpResponse
                        {
                            Body = "Inventory item not found or no inventory levels available.",
                            Status = 400
                        };
                        return response;
                    }

                    foreach (var item in locationResponse.Data.inventoryItem.inventoryLevels.edges)
                    {
                        var location = item?.node?.location;
                        if (location != null)
                        {
                            locationId = location.id.ToString().Replace("gid://shopify/Location/", "");
                            string locationName = location.name;

                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(locationId))
                    {
                        response.HttpResponse = new HttpResponse
                        {
                            Body = "No location found for the inventory item.",
                            Status = 400
                        };
                        return response;
                    }
                }



                string formattedLocationId = $"gid://shopify/Location/{locationId}";

                string mutationQuery = @"
            mutation inventorySetOnHandQuantities($input: InventorySetOnHandQuantitiesInput!) {
                inventorySetOnHandQuantities(input: $input) {
                    userErrors {
                        field
                        message
                    }
                    inventoryAdjustmentGroup {
                        createdAt
                        reason
                        referenceDocumentUri
                        changes {
                            name
                            delta
                        }
                    }
                }
            }";

                var variables = new
                {
                    input = new
                    {
                        reason = "correction",
                        setQuantities = new[]
                        {
                            new
                            {
                                inventoryItemId = inventoryItemId,
                                locationId = formattedLocationId,
                                quantity = quantityInt
                            }
                        }
                    }
                };

                var graphQLResponse = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
                {
                    Query = mutationQuery,
                    Variables = variables
                });

                if (graphQLResponse.Errors != null && graphQLResponse.Errors.Any())
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = string.Join(", ", graphQLResponse.Errors.Select(e => e.Message)),
                        Status = 400
                    };
                    return response;
                }

                var userErrors = graphQLResponse.Data?.inventorySetOnHandQuantities?.userErrors;
                if (userErrors is IEnumerable<dynamic> errors && errors.Any())
                {
                    var errorMessages = errors
                        .Select(e => (string)e.message)
                        .ToList();

                    response.HttpResponse = new HttpResponse
                    {
                        Body = string.Join(", ", errorMessages),
                        Status = 400
                    };
                    return response;
                }


                var inventoryAdjustmentGroup = graphQLResponse.Data?.inventorySetOnHandQuantities?.inventoryAdjustmentGroup;
                if (inventoryAdjustmentGroup != null)
                {
                    var changes = ((IEnumerable<dynamic>)inventoryAdjustmentGroup.changes).ToList();
                    foreach (var change in changes)
                    {
                        response.Items.Add(new ShopifyInventoryLevel
                        {
                            InventoryItemId = variant.InventoryItemId,
                            LocationId = locationId,
                            Available = (int)change.delta,
                            UpdatedAt = inventoryAdjustmentGroup.createdAt,
                            AdminGraphqlApiId = inventoryItemId
                        });
                    }
                }
                else
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = "Error: inventoryAdjustmentGroup is null or missing in the response.",
                        Status = 400
                    };
                }
            }

            return response;
        }


        public async Task<ShopifyInventoryLevelResponse> UpdateStockWithInventoryItemIdAsync(
    string inventoryItemId, string locationId, decimal quantity)
        {
            var response = new ShopifyInventoryLevelResponse();

            if (string.IsNullOrEmpty(inventoryItemId))
            {
                response.HttpResponse = new HttpResponse
                {
                    Body = "Inventory Item ID is required.",
                    Status = 400
                };
                return response;
            }

            if (!int.TryParse(quantity.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture.NumberFormat, out int quantityInt))
            {
                response.HttpResponse = new HttpResponse
                {
                    Body = $"Quantity {quantity} is invalid. Shopify only accepts whole numbers. Please round up or down.",
                    Status = 400
                };
                return response;
            }

            string formattedInventoryItemId = $"gid://shopify/InventoryItem/{inventoryItemId}";

            if (string.IsNullOrEmpty(locationId))
            {
                var locationQuery = @"
        query inventoryItemToProductVariant($inventoryItemId: ID!) {
            inventoryItem(id: $inventoryItemId) {
                id
                inventoryLevels(first: 1) {
                    edges {
                        node {
                            id
                            location {
                                id
                                name
                            }
                        }
                    }
                }
            }
        }";

                var locationVariables = new
                {
                    inventoryItemId = inventoryItemId
                };

                var locationResponse = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
                {
                    Query = locationQuery,
                    Variables = locationVariables
                });

                // Check if there are any errors
                if (locationResponse.Errors != null && locationResponse.Errors.Any())
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = string.Join(", ", locationResponse.Errors.Select(e => e.Message)),
                        Status = 400
                    };
                    return response;
                }

                if (locationResponse.Data == null || locationResponse.Data.inventoryItem == null)
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = "Inventory item not found or no inventory levels available.",
                        Status = 400
                    };
                    return response;
                }

                foreach (var item in locationResponse.Data.inventoryItem.inventoryLevels.edges)
                {
                    var location = item?.node?.location;
                    if (location != null)
                    {
                        locationId = location.id.ToString().Replace("gid://shopify/Location/", "");
                        string locationName = location.name;

                        break;
                    }
                }
                if (string.IsNullOrEmpty(locationId))
                {
                    response.HttpResponse = new HttpResponse
                    {
                        Body = "No location found for the inventory item.",
                        Status = 400
                    };
                    return response;
                }
            }

            string formattedLocationId = $"gid://shopify/Location/{locationId}";

            string mutationQuery = @"
                        mutation inventorySetOnHandQuantities($input: InventorySetOnHandQuantitiesInput!) {
                            inventorySetOnHandQuantities(input: $input) {
                                userErrors {
                                    field
                                    message
                                }
                                inventoryAdjustmentGroup {
                                    createdAt
                                    reason
                                    referenceDocumentUri
                                    changes {
                                        name
                                        delta
                                    }
                                }
                            }
                        }";

            var variables = new
            {
                input = new
                {
                    reason = "correction",
                    setQuantities = new[]
                    {
                        new
                        {
                            inventoryItemId = formattedInventoryItemId,
                            locationId = formattedLocationId,
                            quantity = quantityInt
                        }
                    }
                }
            };

            var graphQLResponse = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
            {
                Query = mutationQuery,
                Variables = variables
            });

            if (graphQLResponse.Errors != null && graphQLResponse.Errors.Any())
            {
                response.HttpResponse = new HttpResponse
                {
                    Body = string.Join(", ", graphQLResponse.Errors.Select(e => e.Message)),
                    Status = 400
                };
                return response;
            }

            var userErrors = graphQLResponse.Data?.inventorySetOnHandQuantities?.userErrors;
            if (userErrors != null && userErrors.Any())
            {
                var errorMessages = ((IEnumerable<dynamic>)userErrors)
                    .Select(e => (string)e.message)
                    .ToList();

                response.HttpResponse = new HttpResponse
                {
                    Body = string.Join(", ", errorMessages),
                    Status = 400
                };
                return response;
            }

            var inventoryAdjustmentGroup = graphQLResponse.Data?.inventorySetOnHandQuantities?.inventoryAdjustmentGroup;

            if (inventoryAdjustmentGroup != null)
            {
                response.HttpResponse = new HttpResponse
                {
                    Body = $"Stock updated successfully. Changes: {string.Join(", ",
                        ((IEnumerable<dynamic>)inventoryAdjustmentGroup.changes)
                        .Select(change => $"{change.name} (Delta: {change.delta})"))}",
                    Status = 200
                };
            }
            else
            {
                response.HttpResponse = new HttpResponse
                {
                    Body = "Failed to update stock. No adjustment group data returned.",
                    Status = 500
                };
            }

            return response;
        }

        public async Task<ShopifyInventoryLevelResponse> SearchInventoryItemsAsync(List<Variant> variants, string sku, List<string> inventoryItemIds)
        {
            if (!string.IsNullOrEmpty(sku))
            {
                inventoryItemIds = variants.Select(a => a.InventoryItemId).ToList();
            }

            if (inventoryItemIds == null || !inventoryItemIds.Any())
            {
                throw new ArgumentException("Inventory item IDs cannot be null or empty.", nameof(inventoryItemIds));
            }

            var query = new StringBuilder("query inventoryItem {");
            for (int i = 0; i < inventoryItemIds.Count; i++)
            {
                query.AppendLine($@"
                item{i + 1}: inventoryItem(id: ""gid://shopify/InventoryItem/{inventoryItemIds[i]}"") {{
                    id
                    sku
                    tracked
                    inventoryLevels(first: 10) {{
                        edges {{
                            node {{
                                location {{
                                    id
                                    name
                                }}
                            }}
                        }}
                    }}
                }}");
            }
            query.AppendLine("}");

            var response = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
            {
                Query = query.ToString()
            });

            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ", response.Errors.Select(e => e.Message)));
            }

            var inventoryResponse = new ShopifyInventoryLevelResponse();
            foreach (var item in response.Data)
            {
                var node = item.Value;
                if (node != null)
                {
                    var locationNode = item.node.location;
                    inventoryResponse.Items.Add(new ShopifyInventoryLevel
                    {
                        InventoryItemId = node.id.ToString().Replace("gid://shopify/InventoryItem/", ""),
                        LocationId = locationNode.id.ToString().Replace("gid://shopify/Location/", ""),
                        Available = item.node.available,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }
            return inventoryResponse;
        }
    }
}
