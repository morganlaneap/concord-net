using Newtonsoft.Json;

namespace ConcordNet.Models.Pact
{
    public class Consumer
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}