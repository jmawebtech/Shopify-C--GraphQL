namespace ShopifyGraphQL.Models
{
    public class BoldMonerisTransaction
    {
        public string charge_id { get; set; }
        public string status { get; set; }
        public string currency_iso_code { get; set; }
        public float amount { get; set; }
        public string gateway_token { get; set; }
    }
}
