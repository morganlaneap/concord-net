using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConcordNet.Models.Pact
{
    public class PactSpecification
    {
        [JsonProperty("consumer")]
        public Consumer Consumer { get; set; }
        
        [JsonProperty("provider")]
        public Provider Provider { get; set; }
        
        [JsonProperty("interactions")]
        public IEnumerable<Interaction> Interactions { get; set; }
    }
}