using Newtonsoft.Json;

namespace ConcordNet.Models.Pact
{
    public class Response
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        
        [JsonProperty("body")]
        public object Body { get; set; }
    }
}