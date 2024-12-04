namespace ShopifyGraphQL.Models
{
    public class MetaData
    {
        [JsonProperty("transaction_fee_tax_amount")]
        public decimal TransactionFeeTaxAmount { get; set; }
        [JsonProperty("transaction_fee_total_amount")]
        public decimal TransactionFeeTotalAmount { get; set; }
    }

}
