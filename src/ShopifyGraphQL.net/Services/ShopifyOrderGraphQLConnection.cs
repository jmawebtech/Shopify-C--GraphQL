
using ShopifyGraphQL.Helper;
using ShopifyGraphQL.Queries;

namespace ShopifyGraphQL.Services
{
    public class ShopifyOrderGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyOrderGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        /// <summary>
        /// Creates new orders. We will send an order object. Low priority method to map.
        /// I can send you some JSON request response from one of our customers.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="existingOrder"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ShopifyOrderResponse> SetOrderAsync(Order order, Order existingOrder)
        {
            ShopifyOrderResponse response = new ShopifyOrderResponse();

            if (existingOrder != null)
            {
                order.Customer = existingOrder.Customer;
            }

            if (existingOrder == null)
            {
                response = await CreateObjectAsync(order);
            }
            else
            {
                response = await UpdateObjectAsync(existingOrder.Id, order, HttpMethodTypes.PUT);
            }
            return response;
        }

        public async Task<ShopifyOrderResponse> CreateObjectAsync(Order order)
        {
            var gqlMutation = @"
    mutation createDraftOrder($input: DraftOrderInput!) {
        draftOrderCreate(input: $input) {
            draftOrder {
                id
                name
                email
                shippingAddress {
                    firstName
                    lastName
                    address1
                    city
                    province
                    country
                    zip
                }
                billingAddress {
                    firstName
                    lastName
                    address1
                    city
                    province
                    country
                    zip
                }
                lineItems(first: 10) {
                    edges {
                        node {
                            title
                            quantity
                            originalUnitPrice
                            sku
                            taxable
                            grams
                        }
                    }
                }
                subtotalPrice
                totalTax
                totalPrice
                createdAt
                updatedAt
            }
            userErrors {
                field
                message
            }
        }
    }";

            var draftOrderInput = new
            {
                input = new
                {
                    email = order.Email,
                    billingAddress = new
                    {
                        firstName = order.BillingAddress.FirstName,
                        lastName = order.BillingAddress.LastName,
                        address1 = order.BillingAddress.Address1,
                        city = order.BillingAddress.City,
                        province = order.BillingAddress.Province,
                        country = order.BillingAddress.Country,
                        zip = order.BillingAddress.Zip
                    },
                    shippingAddress = new
                    {
                        firstName = order.ShippingAddress.FirstName,
                        lastName = order.ShippingAddress.LastName,
                        address1 = order.ShippingAddress.Address1,
                        city = order.ShippingAddress.City,
                        province = order.ShippingAddress.Province,
                        country = order.ShippingAddress.Country,
                        zip = order.ShippingAddress.Zip
                    },
                    lineItems = order.LineItems.Select(item => new
                    {
                        title = item.Title,
                        quantity = item.Quantity,
                        originalUnitPrice = item.Price,
                        sku = item.Sku,
                        taxable = item.Taxable,
                        grams = item.Grams
                    }).ToList(),
                    note = order.Note
                }
            };

            try
            {
                var response = await _client.SendMutationAsync<dynamic>(new GraphQLRequest
                {
                    Query = gqlMutation,
                    Variables = draftOrderInput
                });

                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL API Error(s): {errorMessages}");
                }

                var draftOrder = response.Data?.draftOrderCreate?.draftOrder as JObject;

                if (draftOrder == null)
                {
                    throw new Exception("No order data found in the GraphQL response.");
                }

                var orders = new List<Order>
                {
                    new Order
                    {
                        Id = draftOrder["id"]?.ToString(),
                        Name = draftOrder["name"]?.ToString(),
                        CreatedAt = draftOrder["createdAt"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                        Email = draftOrder["email"]?.ToString(),
                        BillingAddress = MapAddress(draftOrder["billingAddress"] as JObject),
                        ShippingAddress = MapAddress(draftOrder["shippingAddress"] as JObject),
                        LineItems = draftOrder["lineItems"]?["edges"]?.Select(edge => MapLineItem(edge["node"] as JObject)).ToList(),
                        SubtotalPrice = draftOrder["subtotalPrice"]?.ToString(),
                        TotalTax = draftOrder["totalTax"]?.ToObject<decimal>() ?? 0,
                        TotalPrice = draftOrder["totalPrice"]?.ToObject<decimal>() ?? 0
                    }
                };

                return new ShopifyOrderResponse { Items = orders };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating draft order: {ex.Message}");
                throw;
            }
        }

        public async Task<ShopifyOrderResponse> UpdateObjectAsync(string id, Order order, HttpMethodTypes httpMethodType)
        {
            try
            {
                if (httpMethodType != HttpMethodTypes.PUT)
                {
                    throw new ArgumentException("Invalid HTTP method type for updating an object. Use PUT.");
                }

                var gqlMutation = @"
mutation updateOrderDetails($input: OrderInput!) {
    orderUpdate(input: $input) {
        order {
            id
            name
            email
            subtotalPrice
            totalTax
            totalPrice
            updatedAt
        }
        userErrors {
            field
            message
        }
    }
}";

                var orderInput = new
                {
                    input = new
                    {
                        id = id,
                        email = order.Email,
                        note = order.Note
                    }
                };

                var response = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
                {
                    Query = gqlMutation,
                    Variables = orderInput
                });

                // Handle GraphQL errors
                if (response.Errors != null && response.Errors.Any())
                {
                    var errorMessages = string.Join(", ", response.Errors.Select(e => e.Message));
                    throw new Exception($"GraphQL API Error(s): {errorMessages}");
                }

                var orderData = response.Data?.orderUpdate?.order;
                if (orderData == null)
                {
                    throw new Exception("No order data found in the GraphQL response.");
                }

                var orders = new List<Order>
                    {
                        new Order
                        {
                            Id = orderData.id,
                            Name = orderData.name,
                            Email = orderData.email,
                            SubtotalPrice = orderData.subtotalPrice,
                            TotalTax = orderData.totalTax,
                            TotalPrice = orderData.totalPrice,
                            UpdatedAt = orderData.updatedAt
                        }
                    };

                return new ShopifyOrderResponse { Items = orders };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating draft order: {ex.Message}");
                throw;
            }
        }
        private List<Order> ExtractOrdersFromResponse(dynamic responseData)
        {
            var orders = new List<Order>();

            var orderEdges = responseData?.orders?.edges;
            if (orderEdges == null) return orders;

            foreach (var edge in orderEdges)
            {
                var sourceOrder = edge.node as JObject;
                if (sourceOrder == null) continue;

                var order = new Order
                {
                    Id = sourceOrder["id"]?.ToString(),
                    Name = sourceOrder["name"]?.ToString(),
                    CreatedAt = sourceOrder["createdAt"]?.ToObject<DateTime>() ?? DateTime.MinValue,
                    Email = sourceOrder["email"]?.ToString(),
                    BillingAddress = MapAddress(sourceOrder["billingAddress"] as JObject),
                    ShippingAddress = MapAddress(sourceOrder["shippingAddress"] as JObject),
                    LineItems = sourceOrder["lineItems"]?["edges"]?.Select(edge => MapLineItem(edge["node"] as JObject)).ToList(),
                    CurrentTotalPriceSet = MapPriceSet(sourceOrder["currentTotalPriceSet"])
                };

                orders.Add(order);
            }

            return orders;
        }

        public async Task<ShopifyOrderResponse> GetByIdAsync(string orderId)
        {
            var query = new ShopifyOrderQuery();
            query.Id = orderId;

            var response = await SearchAsync(query);

            return response;
        }

        public async Task<ShopifyOrderResponse> SearchOrdersByIdOrOrderNumberAsync(List<string> orderIdsOrNumbers)
        {
            var orderResponse = new ShopifyOrderResponse();

            foreach (string orderIdOrNumber in orderIdsOrNumbers)
            {
                if (String.IsNullOrEmpty(orderIdOrNumber))
                {
                    continue;
                }
                if (orderIdOrNumber.Length <= 10 || Regex.Matches(orderIdOrNumber, @"[a-zA-Z]").Count > 0)
                {
                    var query = new ShopifyOrderQuery() { OrderNumber = orderIdOrNumber };
                    var shopifyOrderNumberResponse = await SearchAsync(query);

                    orderResponse.Items.AddRange(shopifyOrderNumberResponse.Items);
                }
                else
                {
                    var response = await GetByIdAsync(orderIdOrNumber);

                    if (!String.IsNullOrEmpty(response.Item.Id))
                    {
                        orderResponse.Items.Add(response.Item);
                    }
                }
            }

            return orderResponse;
        }

        public async Task<ShopifyOrderResponse> SearchAsync(ShopifyOrderQuery query)
        {
            var queryString = new List<string>();

            if (!string.IsNullOrEmpty(query.OrderNumber))
            {
                queryString.Add($"name:{query.OrderNumber.Replace("#", string.Empty)}");
            }

            if (!string.IsNullOrEmpty(query.Title))
            {
                queryString.Add($"title:{query.Title}");
            }

            if (query.ProcessedAtMin.HasValue)
            {
                string processedMin = query.ProcessedAtMin.Value.ToString("yyyy-MM-dd HH:mm:ss");
                queryString.Add($"processed_at_min:{processedMin}");
            }

            if (query.ProcessedAtMax.HasValue)
            {
                string processedMax = query.ProcessedAtMax.Value.ToString("yyyy-MM-dd HH:mm:ss");
                queryString.Add($"processed_at_max:{processedMax}");
            }

            if (!string.IsNullOrEmpty(query.OrderStatus))
            {
                if (IsShippedStatus(query.OrderStatus))
                {
                    queryString.Add($"fulfillment_status:{GetFulfillmentStatus(query.OrderStatus)}");
                }
                else if (IsGlobalStatus(query.OrderStatus))
                {
                    queryString.Add($"status:{query.OrderStatus.ToLower()}");
                }
                else
                {
                    string financialStatus = EnumHelper.GetString(typeof(FinancialStatus), query.OrderStatus);
                    queryString.Add($"financial_status:{financialStatus}");
                }
            }

            if (query.CreatedAtMin.HasValue)
            {
                string createdMin = query.CreatedAtMin.Value.ToString("yyyy-MM-dd HH:mm:ss");
                queryString.Add($"created_at_min:{createdMin}");
            }

            if (query.CreatedAtMax.HasValue)
            {
                string createdMax = query.CreatedAtMax.Value.ToString("yyyy-MM-dd HH:mm:ss");
                queryString.Add($"created_at_max:{createdMax}");
            }

            if (query.UpdatedAtMin.HasValue)
            {
                string updatedMin = query.UpdatedAtMin.Value.ToString("yyyy-MM-dd HH:mm:ss");
                queryString.Add($"updated_at_min:{updatedMin}");
            }

            if (query.UpdatedAtMax.HasValue)
            {
                string updatedMax = query.UpdatedAtMax.Value.ToString("yyyy-MM-dd HH:mm:ss");
                queryString.Add($"updated_at_max:{updatedMax}");
            }

            if (!string.IsNullOrEmpty(query.Id))
            {
                queryString.Add($"id:{query.Id}");
            }

            if (!string.IsNullOrEmpty(query.Fields))
            {
                queryString.Add($"fields:{query.Fields}");
            }

            if (query.ResultsPerPage.HasValue)
            {
                queryString.Add($"limit:{query.ResultsPerPage.Value}");
            }

            var finalQuery = queryString.Any() ? string.Join(" OR ", queryString) : "status:any";

            var gqlQuery = $@"
    query ($first: Int, $after: String) {{
        orders(first: $first, after: $after, query: ""{finalQuery}"") {{
            edges {{
            node {{
                id
                name
                createdAt
                email
                displayFinancialStatus
                displayFulfillmentStatus
                cancelReason
                cancelledAt
                closedAt
                confirmed
                currentTotalDiscountsSet {{
                    presentmentMoney {{
                        amount
                        currencyCode
                    }}
                    shopMoney {{
                        amount
                        currencyCode
                    }}
                }}
                currentSubtotalPriceSet {{
                    presentmentMoney {{
                        amount
                        currencyCode
                    }}
                    shopMoney {{
                        amount
                        currencyCode
                    }}
                }}
                currentTotalPriceSet {{
                    presentmentMoney {{
                        amount
                        currencyCode
                    }}
                    shopMoney {{
                        amount
                        currencyCode
                    }}
                }}
                currentTotalTaxSet {{
                    presentmentMoney {{
                        amount
                        currencyCode
                    }}
                    shopMoney {{
                        amount
                        currencyCode
                    }}
                }}
                discountApplications(first: 10) {{
                    edges {{
                        node {{
                            ... on ManualDiscountApplication {{
                                title
                                description
                            }}
                            ... on DiscountCodeApplication {{
                                code
                                allocationMethod
                                targetSelection
                                targetType
                            }}
                            ... on AutomaticDiscountApplication {{
                                title
                            }}
                        }}
                    }}
                }}
                tags
                lineItems(first: 10) {{
                    edges {{
                        node {{
                            id
                            title
                            quantity
                            originalUnitPriceSet {{
                                shopMoney {{
                                    amount
                                    currencyCode
                                }}
                                presentmentMoney {{
                                    amount
                                    currencyCode
                                }}
                            }}
                            discountedUnitPriceSet {{
                                shopMoney {{
                                    amount
                                    currencyCode
                                }}
                                presentmentMoney {{
                                    amount
                                    currencyCode
                                }}
                            }}
                        }}
                    }}
                }}
                shippingAddress {{
                    firstName
                    lastName
                    address1
                    address2
                    city
                    country
                    zip
                }}
                billingAddress {{
                    firstName
                    lastName
                    address1
                    address2
                    city
                    country
                    zip
                }}
            }}
        }}
        pageInfo {{
            hasNextPage
            endCursor
        }}
    }}
}}";

            var allOrders = new List<Order>();

            string afterCursor = null;
            bool hasNextPage = true;
            while (hasNextPage)
            {
                var response = await CallGraphQLApi(gqlQuery, query.ResultsPerPage ?? 250, afterCursor);

                if (response?.Items != null)
                {
                    allOrders.AddRange(response.Items);
                }

                afterCursor = (bool)response?.HasNextPage ? (string)response?.NextPageCursor : null;
                if (string.IsNullOrEmpty(afterCursor))
                {
                    hasNextPage = false;
                }
            }

            return new ShopifyOrderResponse { Items = allOrders };
        }





        public async Task<ShopifyOrderResponse> CallGraphQLApi(string gqlQuery, int? first = null, string after = null)
        {
            try
            {
                var response = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
                {
                    Query = gqlQuery,
                    Variables = new { first, after }
                });

                if (response.Errors != null && response.Errors.Any())
                {
                    throw new Exception($"GraphQL errors: {string.Join(", ", response.Errors.Select(e => e.Message))}");
                }

                var orderData = response.Data?.orders?.edges;

                if (orderData == null)
                {
                    throw new Exception("No order data found in the GraphQL response.");
                }

                var orders = ExtractOrdersFromResponse(response.Data);

                var pageInfo = response.Data?.orders?.pageInfo;
                string nextPageCursor = pageInfo?.endCursor;
                bool hasNextPage = pageInfo?.hasNextPage ?? false;

                return new ShopifyOrderResponse
                {
                    Items = orders,
                    HasNextPage = hasNextPage,
                    NextPageCursor = nextPageCursor
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling GraphQL API", ex);
            }
        }


        private ShopifyPriceSet MapPriceSet(JToken priceSet)
        {
            if (priceSet == null)
            {
                return null; // or you can return a default value if needed
            }
            return new ShopifyPriceSet
            {
                PresentmentMoney = new ShopifyMoney
                {
                    Amount = Convert.ToDecimal(priceSet["presentmentMoney"]?["amount"]?.ToString()),
                    CurrencyCode = priceSet["presentmentMoney"]?["currencyCode"]?.ToString()
                },
                ShopMoney = new ShopifyMoney
                {
                    Amount = Convert.ToDecimal(priceSet["shopMoney"]?["amount"]?.ToString()),
                    CurrencyCode = priceSet["shopMoney"]?["currencyCode"]?.ToString()
                }
            };
        }

        private Address MapAddress(JObject addressData)
        {
            if (addressData == null) return null;

            return new Address
            {
                FirstName = addressData["firstName"]?.ToString(),
                LastName = addressData["lastName"]?.ToString(),
                Address1 = addressData["address1"]?.ToString(),
                City = addressData["city"]?.ToString(),
                Province = addressData["province"]?.ToString(),
                Country = addressData["country"]?.ToString(),
                Zip = addressData["zip"]?.ToString()
            };
        }

        private LineItem MapLineItem(JObject lineItemData)
        {
            if (lineItemData == null) return null;

            return new LineItem
            {
                Id = lineItemData["id"]?.ToString(),
                Title = lineItemData["title"]?.ToString(),
                Quantity = lineItemData["quantity"]?.ToObject<int>() ?? 0,
                PriceSet = MapPriceSet(lineItemData["originalUnitPriceSet"]),
                Taxable = lineItemData["taxable"]?.ToObject<bool>() ?? false
            };
        }
        private bool IsGlobalStatus(string statusName)
        {
            string[] statuses = new string[] { "open", "closed" };
            return !string.IsNullOrEmpty(statusName) && statuses.Contains(statusName.Trim().ToLower());
        }

        private bool IsShippedStatus(string statusName)
        {
            string[] shippedStatuses = new string[] { "fulfilled", "shipped", "partial", "unshipped" };
            return !string.IsNullOrEmpty(statusName) && shippedStatuses.Contains(statusName.Trim().ToLower());
        }

        private string GetFulfillmentStatus(string statusName)
        {
            if (statusName == "fulfilled")
                return "shipped";
            if (statusName == "partial")
                return "partial";
            return EnumHelper.GetString(typeof(FulfillmentStatus), statusName.ToString());
        }
    }
}
