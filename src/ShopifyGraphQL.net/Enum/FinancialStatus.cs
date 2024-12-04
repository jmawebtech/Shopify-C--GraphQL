
namespace ShopifyGraphQL.Enum
{
    public enum FinancialStatus
    {
        [EnumMember(Value = "paid")]
        Paid,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "authorized")]
        Authorized,
        [EnumMember(Value = "partially_paid")]
        PartiallyPaid,
        [EnumMember(Value = "partially_refunded")]
        PartiallyRefunded,
        [EnumMember(Value = "refunded")]
        Refunded,
        [EnumMember(Value = "voided")]
        Voided,
        [EnumMember(Value = "Unknown")]
        Unknown
    }
}
