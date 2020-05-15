using Newtonsoft.Json;

namespace ConcordNet.Models.Pact
{
    public class Interaction
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("request")]
        public Request Request { get; set; }
        
        [JsonProperty("response")]
        public Response Response { get; set; }
    }
}