using Newtonsoft.Json;

namespace ConcordNet.Models
{
    public class ContractRequest
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}