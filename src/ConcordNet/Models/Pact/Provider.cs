using Newtonsoft.Json;

namespace ConcordNet.Models.Pact
{
    public class Provider
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}