using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyGraphQL.Models
{
    public class Webhook
    {

        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("topic")]
        public string? Topic { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("fields")]
        public IList<object> Fields { get; set; } = default!;

        [JsonProperty("metafield_namespaces")]
        public IList<object> MetafieldNamespaces { get; set; } = default!;

        [JsonProperty("api_version")]
        public string? ApiVersion { get; set; }

        [JsonProperty("private_metafield_namespaces")]
        public IList<object> PrivateMetafieldNamespaces { get; set; } = default!;
    }
}
