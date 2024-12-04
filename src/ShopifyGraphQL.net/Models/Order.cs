namespace ShopifyGraphQL.Models
{
    public class Order
    {
        public Order()
        {
            Customer = new Customer();
            DiscountApplications = new List<DiscountApplications>();
            NoteAttributes = new List<NoteAttribute>();
            Refunds = new List<Refund>();
            LineItems = new List<LineItem>();
            ShippingLines = new List<ShippingLine>();
            TaxLines = new List<TaxLine>();
            DiscountCodes = new List<DiscountCodes>();
            PaymentGatewayNames = new List<string>();
        }

        [JsonProperty("buyer_accepts_marketing")]
        public bool BuyerAcceptsMarketing { get; set; }
        [JsonProperty("cancel_reason")]
        public string CancelReason { get; set; }
        [JsonProperty("cancelled_at")]
        public DateTime? CancelledAt { get; set; }
        [JsonProperty("cart_token")]
        public string CartToken { get; set; }
        [JsonProperty("checkout_token")]
        public string CheckoutToken { get; set; }
        [JsonProperty("closed_at")]
        public DateTime? ClosedAt { get; set; }
        [JsonProperty("confirmed")]
        public bool Confirmed { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("current_total_discounts_set")]
        public ShopifyPriceSet CurrentDiscountsSet { get; set; }
        [JsonProperty("current_subtotal_price_set")]
        public ShopifyPriceSet CurrentSubTotalPriceSet { get; set; }
        [JsonProperty("current_total_price_set")]
        public ShopifyPriceSet CurrentTotalPriceSet { get; set; }
        [JsonProperty("current_total_tax_set")]
        public ShopifyPriceSet CurrentTotalTaxSet { get; set; }
        [JsonProperty("discount_applications")]
        public List<DiscountApplications> DiscountApplications { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("financial_status")]
        public FinancialStatus FinancialStatus { get; set; }
        [JsonProperty("fulfillment_status")]
        public FulfillmentStatus? FulfillmentStatus { get; set; }
        [JsonProperty("gateway")]
        public string Gateway { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("landing_site")]
        public string LandingSite { get; set; }
        [JsonProperty("location_id")]
        public string LocationId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("processed_at")]
        public DateTime ProcessedAt { get; set; }
        [JsonProperty("reference")]
        public string Reference { get; set; }
        [JsonProperty("referring_site")]
        public string ReferringSite { get; set; }
        [JsonProperty("source_identifier")]
        public string SourceIdentifier { get; set; }
        [JsonProperty("source_name")]
        public string SourceName { get; set; }
        [JsonProperty("source_url")]
        public object SourceURL { get; set; }
        [JsonProperty("subtotal_price")]
        public string SubtotalPrice { get; set; }
        [JsonProperty("status")]
        public ShopifyOrderStatus? Status { get; set; }
        [JsonProperty("taxes_included")]
        public bool TaxesIncluded { get; set; }
        [JsonProperty("test")]
        public bool Test { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("total_discounts")]
        public decimal TotalDiscounts { get; set; }
        [JsonProperty("total_line_items_price")]
        public decimal TotalLineItemsPrice { get; set; }
        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }
        [JsonProperty("total_price_usd")]
        public decimal TotalPriceUSD { get; set; }
        [JsonProperty("total_tax")]
        public decimal TotalTax { get; set; }
        [JsonProperty("total_weight")]
        public string TotalWeight { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("browser_ip")]
        public string BrowserIp { get; set; }
        [JsonProperty("landing_site_ref")]
        public object LandingSiteRef { get; set; }
        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }
        [JsonProperty("discount_codes")]
        public List<DiscountCodes> DiscountCodes { get; set; }
        [JsonProperty("note_attributes")]
        public List<NoteAttribute> NoteAttributes { get; set; }
        [JsonProperty("processing_method")]
        public string ProcessingMethod { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("checkout_id")]
        public string CheckoutId { get; set; }
        [JsonProperty("tax_lines")]
        public List<TaxLine> TaxLines { get; set; }
        [JsonProperty("tags")]
        public string Tags { get; set; }
        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }
        [JsonProperty("shipping_lines")]
        public List<ShippingLine> ShippingLines { get; set; }
        [JsonProperty("billing_address")]
        public Address BillingAddress { get; set; }
        [JsonProperty("shipping_address")]
        public Address ShippingAddress { get; set; }
        [JsonProperty("fulfillments")]
        public List<Fulfillment> Fulfillments = new List<Fulfillment>();
        [JsonProperty("client_details")]
        public ClientDetails ClientDetails { get; set; }
        [JsonProperty("refunds")]
        public List<Refund> Refunds { get; set; }
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
        [JsonProperty("payment_details")]
        public PaymentDetails PaymentDetails { get; set; }
        [JsonProperty("payment_gateway_names")]
        public List<string> PaymentGatewayNames { get; set; }
        [JsonProperty("original_total_duties_set")]
        public ShopifyPriceSet OriginalTotalDutiesSet { get; set; }
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    }
}
