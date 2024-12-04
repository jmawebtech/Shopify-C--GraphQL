namespace ShopifyGraphQL.Enum
{
    public enum ShopifyOrderAdjustmentKind
    {
        [EnumMember(Value = "shipping_refund")]
        ShippingRefund,
        [EnumMember(Value = "refund_discrepancy")]
        RefundDiscrepancy,

    }
}
