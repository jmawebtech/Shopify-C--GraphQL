namespace ShopifyGraphQL.Models
{
    public class Receipt
    {
        public Receipt()
        {
            BoldMonerisTransaction = new BoldMonerisTransaction();
            CardDetails = new CardDetails();
            Charges = new Charges();
        }

        [JsonProperty("processed_by")]
        public string ProcessedBy { get; set; }
        [JsonProperty("bold_moneris_transaction")]
        public BoldMonerisTransaction BoldMonerisTransaction { get; set; }
        [JsonProperty("card_details")]
        public CardDetails CardDetails { get; set; }
        [JsonProperty("charges")]
        public Charges Charges { get; set; }
    }
}
