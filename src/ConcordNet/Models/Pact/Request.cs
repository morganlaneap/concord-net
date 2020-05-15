using Newtonsoft.Json;

namespace ConcordNet.Models.Pact
{
    public class Request
    {
        [JsonProperty("method")]
        public string Method { get; set; }
        
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}