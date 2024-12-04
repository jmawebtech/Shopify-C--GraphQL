namespace ShopifyGraphQL.Enum
{
    public enum PayoutTransactionType
    {
        [EnumMember(Value = "charge")]
        Charge,
        [EnumMember(Value = "refund")]
        Refund,
        [EnumMember(Value = "dispute")]
        Dispute,
        [EnumMember(Value = "reserve")]
        Reserve,
        [EnumMember(Value = "adjustment")]
        Adjustment,
        [EnumMember(Value = "credit")]
        Credit,
        [EnumMember(Value = "debit")]
        Debit,
        [EnumMember(Value = "payout")]
        Payout,
        [EnumMember(Value = "payout_failure")]
        PayoutFailure,
        [EnumMember(Value = "refund_failure")]
        RefundFailure,
        [EnumMember(Value = "payout_cancellation")]
        PayoutCancellation
    }
}
