using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConcordNet.Models
{
    public class ContractDefinition
    {
        [JsonProperty("provider")]
        public string Provider { get; set; }
        
        [JsonProperty("consumer")]
        public string Consumer { get; set; }
        
        [JsonProperty("contracts")]
        public IEnumerable<Contract> Contracts { get; set; }
    }
}