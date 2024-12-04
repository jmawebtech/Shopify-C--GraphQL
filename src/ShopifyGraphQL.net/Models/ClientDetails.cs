namespace ShopifyGraphQL.Models
{
    public class ClientDetails
    {
        [JsonProperty("accept_language")]
        public string AcceptLanguage { get; set; }
        [JsonProperty("browser_height")]
        public object BrowserHeight { get; set; }
        [JsonProperty("browser_ip")]
        public string BrowserIp { get; set; }
        [JsonProperty("browser_width")]
        public object BrowserWidth { get; set; }
        [JsonProperty("session_hash")]
        public string SessionHash { get; set; }
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }
    }
}
