namespace ShopifyGraphQL.Enum
{
    public enum FulfillmentStatus
    {
        [EnumMember(Value = "fulfilled")]
        Fulfilled,
        [EnumMember(Value = "partial")]
        Partial,
        [EnumMember(Value = "restocked")]
        Restocked,
        [EnumMember(Value = "unshipped")]
        Unshipped,
    }
}
