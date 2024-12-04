namespace ShopifyGraphQL.Models
{
    public class RefundLineItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("line_item_id")]
        public string LineItemId { get; set; }
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }
        [JsonProperty("restock_type")]
        public RefundLineItemRestockTypeEnum RestockType { get; set; }
        [JsonProperty("line_item")]
        public LineItem LineItem { get; set; }
        [JsonProperty("total_tax")]
        public decimal? TotalTax { get; set; }
        [JsonProperty("subtotal")]
        public decimal? Subtotal { get; set; }
    }
}
