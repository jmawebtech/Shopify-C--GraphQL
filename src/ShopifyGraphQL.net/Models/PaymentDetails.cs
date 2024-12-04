namespace ShopifyGraphQL.Models
{
    public class PaymentDetails
    {
        [JsonProperty("avs_result_code")]
        public string AVSResultCode { get; set; }
        [JsonProperty("credit_card_bin")]
        public string CreditCardBin { get; set; }
        [JsonProperty("cvv_result_code")]
        public string CVVResultCode { get; set; }
        [JsonProperty("credit_card_number")]
        public string CreditCardNumber { get; set; }
        [JsonProperty("credit_card_company")]
        public string CreditCardCompany { get; set; }
    }
}
