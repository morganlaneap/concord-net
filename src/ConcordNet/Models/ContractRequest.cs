using ConcordNet.Models.Pact;
using Newtonsoft.Json;

namespace ConcordNet.Models
{
    public class ContractRequest
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("method")]
        public string Method { get; set; }

        public static ContractRequest FromPactInteractionRequest(Request request)
        {
            return new ContractRequest
            {
                Url = request.Path,
                Method = request.Method
            };
        }
    }
}