using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace ConcordNet.Models
{
    public class ContractResponse
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }
        
        [JsonProperty("headers")]
        public IEnumerable<string> Headers { get; set; }
        
        [JsonProperty("body")]
        public object Body { get; set; }
    }
}