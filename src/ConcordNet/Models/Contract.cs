using Newtonsoft.Json;

namespace ConcordNet.Models
{
    public class Contract
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("scenario")]
        public string Scenario { get; set; }
        
        [JsonProperty("request")]
        public ContractRequest Request { get; set; }
        
        [JsonProperty("response")]
        public ContractResponse Response { get; set; }
    }
}