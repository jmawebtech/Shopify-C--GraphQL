namespace ShopifyGraphQL.Enum
{
    public enum ShopifyOrderStatus
    {
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "invoice_sent")]
        InvoiceSent,
        [EnumMember(Value = "completed")]
        Completed,
        [EnumMember(Value = "any")]
        Any,
        [EnumMember(Value = "closed")]
        Closed,
        [EnumMember(Value = "cancelled")]
        Cancelled,
        [EnumMember(Value = "draft_open")]
        DraftOpen,
    }
}
