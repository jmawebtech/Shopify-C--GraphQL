namespace ShopifyGraphQL.Models
{
    public class DiscountApplications
    {
        [JsonProperty("target_type")]
        public TargetTypeEnum TargetType { get; set; }
        [JsonProperty("target_selection")]
        public string TargetSelection { get; set; }
        [JsonProperty("allocation_method")]
        public AllocationMethodEnum AllocationMethod { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("value")]
        public decimal Value { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
