namespace ShopifyGraphQL.Enum
{
    public enum VerbEnum
    {
        Create, Destroy, Published, Unpublished, Update, Confirmed, Placed,
        [EnumMember(Value = "fulfillment_success")]
        FulfillmentSuccess
    }
}
