using GraphQL;

namespace ShopifyGraphQL.Services
{
    public class ShopifyInventoryItemGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyInventoryItemGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        /// <summary>
        /// Low priority. Not sure if this code is used. If not, do not bother.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<HttpResponse> AddTrackingAndCostAsync(string itemId, ShopifyInventoryItem jsonBody)
        {
            var query = @"
            mutation inventoryItemUpdate($id: ID!, $input: InventoryItemUpdateInput!) {
              inventoryItemUpdate(id: $id, input: $input) {
                inventoryItem {
                  id
                  unitCost {
                    amount
                  }
                  tracked
                  countryCodeOfOrigin
                  provinceCodeOfOrigin
                  harmonizedSystemCode
                  countryHarmonizedSystemCodes(first: 1) {
                    edges {
                      node {
                        harmonizedSystemCode
                        countryCode
                      }
                    }
                  }
                }
                userErrors {
                  message
                }
              }
            }";

            var variables = new
            {
                id = itemId,
                input = new
                {
                    cost = jsonBody.Cost,  
                    tracked = jsonBody.Tracked 
                }
            };

            var graphQLRequest = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };

            var graphQLResponse = await _client.SendQueryAsync<dynamic>(graphQLRequest);

            if (graphQLResponse.Errors != null && graphQLResponse.Errors.Any())
            {
                return new HttpResponse
                {
                    Body = string.Join(", ", graphQLResponse.Errors.Select(e => e.Message)),
                    Status = 400
                };
            }
            return new HttpResponse
            {
                Body = "Successfully updated inventory item.",
                Status = 200
            };
        }


    }
}