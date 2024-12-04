using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyGraphQL.Models
{
    public class ShopifyInstall
    {
        public string code { get; set; } = string.Empty;
        public string shop { get; set; } = string.Empty;
    }

    public class ShopifyAppPurchaseReturnDto
    {
        public string authorizationToken { get; set; } = string.Empty;
        public string shop { get; set; } = string.Empty;
        public string redirectUrl { get; set; } = string.Empty;
    }
}
