namespace ShopifyGraphQL.Services
{
    public class ShopifyTransactionGraphQLConnection : ShopifyGraphQLConnection
    {
        public ShopifyTransactionGraphQLConnection(string shopName, string accessToken) : base(shopName, accessToken)
        {

        }

        /// <summary>
        /// Low priority method.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<HttpResponse> CapturePendingTransactionAsync(Order order)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Low priority method
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<HttpResponse> CapturePendingTransactionAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponse> SearchTransactionsAsync(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
