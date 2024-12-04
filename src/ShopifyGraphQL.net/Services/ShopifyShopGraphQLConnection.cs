namespace ShopifyGraphQL.Services
{
    public class ShopifyShopGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyShopGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        public async Task<ShopifyShopResponse> SearchAsync()
        {
            var query = @"
            query ShopShow {
              shop {
                id
                name
                email
                primaryDomain {
                  host
                }
                billingAddress {
                  province
                  country
                  address1
                  zip
                  city
                  address2
                  phone
                  latitude
                  longitude
                  countryCodeV2
                  provinceCode
                }
                currencyCode
                contactEmail
                ianaTimezone
                currencyFormats {
                  moneyFormat
                  moneyWithCurrencyFormat
                  moneyInEmailsFormat
                  moneyWithCurrencyInEmailsFormat
                }
                weightUnit
                taxesIncluded
                taxShipping
                plan {
                  displayName
                }
                myshopifyDomain
                checkoutApiSupported
                setupRequired
              }
            }";

            var response = await _client.SendQueryAsync<dynamic>(new GraphQLRequest
            {
                Query = query
            });

            if (response.Errors != null && response.Errors.Any())
            {
                throw new Exception(string.Join(", ", response.Errors.Select(e => e.Message)));
            }

            var shopData = response.Data?.shop;
            if (shopData == null)
            {
                throw new Exception("Shop details not found in the response.");
            }
            var shop = new Shop
            {
                Id = shopData.id,
                Name = shopData.name,
                Email = shopData.email,
                Domain = shopData.primaryDomain?.host,
                Province = shopData.billingAddress?.province,
                Country = shopData.billingAddress?.country,
                Address1 = shopData.billingAddress?.address1,
                Zip = shopData.billingAddress?.zip,
                City = shopData.billingAddress?.city,
                Address2 = shopData.billingAddress?.address2,
                Phone = shopData.billingAddress?.phone,
                Latitude = shopData.billingAddress?.latitude,
                Longitude = shopData.billingAddress?.longitude,
                ProvinceCode = shopData.billingAddress?.provinceCode,
                CountryCode = shopData.billingAddress?.countryCodeV2,
                Currency = shopData.currencyCode,
                CustomerEmail = shopData.contactEmail,
                IanaTimezone = shopData.ianaTimezone,
                ShopOwner = shopData.shopOwner?.name,
                MoneyFormat = shopData.currencyFormats?.moneyFormat,
                MoneyWithCurrencyFormat = shopData.currencyFormats?.moneyWithCurrencyFormat,
                MoneyInEmailsFormat = shopData.currencyFormats?.moneyInEmailsFormat,
                MoneyWithCurrencyInEmailsFormat = shopData.currencyFormats?.moneyWithCurrencyInEmailsFormat,
                WeightUnit = shopData.weightUnit,
                TaxesIncluded = shopData.taxesIncluded,
                TaxShipping = shopData.taxShipping,
                PlanDisplayName = shopData.plan?.displayName,
                MyshopifyDomain = shopData.myshopifyDomain,
                CheckoutApiSupported = shopData.checkoutApiSupported,
                SetupRequired = shopData.setupRequired
            };

            return new ShopifyShopResponse
            {
                HttpResponse = new HttpResponse
                {
                    Status = 200,
                    Body = "Shop details retrieved successfully."
                },
                Item = shop
            };
        }

       


    }
}
