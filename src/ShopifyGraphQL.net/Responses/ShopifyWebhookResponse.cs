using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyGraphQL.Responses
{
    public class ShopifyWebhookResponse : ApiResponse
    {
        [JsonProperty("webhook")]
        public Webhook? Item { get; set; }

        [JsonProperty("webhooks")]
        public List<Webhook> Items { get; set; } = new List<Webhook>();
    }
}
