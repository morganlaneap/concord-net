using System.Collections.Generic;
using System.Net;
using ConcordNet.Models.Pact;
using Newtonsoft.Json;

namespace ConcordNet.Models
{
    public class ContractResponse
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }
        
        [JsonProperty("headers")]
        public Dictionary<string,string> Headers { get; set; }
        
        [JsonProperty("body")]
        public object Body { get; set; }

        public static ContractResponse FromPactInteractionResponse(Response response)
        {
            return new ContractResponse
            {
                StatusCode = (HttpStatusCode)response.Status,
                Body = response.Body
            };
        }
    }
}