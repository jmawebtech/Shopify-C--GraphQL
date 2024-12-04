namespace ShopifyGraphQL.Models
{
    public class LineItem
    {
        public LineItem()
        {
            AppliedDiscount = new AppliedDiscount();
            TaxLines = new List<TaxLine>();
            Properties = new List<Properties>();
            OriginLocation = new Location();
            DiscountAllocations = new List<DiscountAllocations>();
        }

        [JsonProperty("applied_discount")]
        public AppliedDiscount AppliedDiscount { get; set; }
        [JsonProperty("fulfillment_service")]
        public string FulfillmentService { get; set; }
        [JsonProperty("fulfillment_status")]
        public object FulfillmentStatus { get; set; }
        [JsonProperty("gift_card")]
        public bool GiftCard { get; set; }
        [JsonProperty("grams")]
        public string Grams { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("product_id")]
        public string ProductId { get; set; }
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }
        [JsonProperty("requires_shipping")]
        public bool? RequiresShipping { get; set; }
        [JsonProperty("sku")]
        public string Sku { get; set; }
        [JsonProperty("taxable")]
        public bool Taxable { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("variant_id")]
        public string VariantId { get; set; }
        [JsonProperty("variant_title")]
        public string VariantTitle { get; set; }
        [JsonProperty("vendor")]
        public string Vendor { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price_set")]
        public ShopifyPriceSet PriceSet { get; set; }
        [JsonProperty("variant_inventory_management")]
        public string VariantInventoryManagement { get; set; }
        [JsonProperty("properties")]
        public List<Properties> Properties { get; set; }
        [JsonProperty("product_exists")]
        public bool ProductExists { get; set; }
        [JsonProperty("fulfillable_quantity")]
        public string FulfillableQuantity { get; set; }
        [JsonProperty("tax_lines")]
        public List<TaxLine> TaxLines { get; set; }
        [JsonProperty("total_discount")]
        public decimal? TotalDiscount { get; set; }
        [JsonProperty("origin_location")]
        public Location OriginLocation { get; set; }
        [JsonProperty("discount_allocations")]
        public List<DiscountAllocations> DiscountAllocations { get; set; }
        [JsonProperty("product_type")]
        public string ProductType { get; set; }


    }
}
