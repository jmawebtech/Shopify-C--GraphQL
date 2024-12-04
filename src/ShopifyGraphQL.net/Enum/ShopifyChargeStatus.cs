namespace ShopifyGraphQL.Enum
{
    public enum ShopifyChargeStatus
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "accepted")]
        Accepted,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "declined")]
        Declined,
        [EnumMember(Value = "expired")]
        Expired,
        [EnumMember(Value = "frozen")]
        Frozen,
        [EnumMember(Value = "cancelled")]
        Cancelled
    }
}
