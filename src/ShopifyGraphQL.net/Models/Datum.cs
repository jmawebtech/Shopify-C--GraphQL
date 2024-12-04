namespace ShopifyGraphQL.Models
{

    public class Datum
    {
        [JsonProperty("application_fee")]
        public string ApplicationFee { get; set; }

        /// <summary>
        /// Can be a string or it can be an object
        /// </summary>
        [JsonProperty("balance_transaction")]
        public object BalanceTransaction { get; set; }

        [JsonProperty("metadata")]
        public MetaData MetaData { get; set; }
    }

}
