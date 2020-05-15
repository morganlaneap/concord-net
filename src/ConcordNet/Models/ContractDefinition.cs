using System.Collections.Generic;
using System.Linq;
using ConcordNet.Models.Pact;
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

        public static ContractDefinition FromPactSpecification(PactSpecification pactSpecification)
        {
            return new ContractDefinition
            {
                Consumer = pactSpecification.Consumer.Name,
                Provider = pactSpecification.Provider.Name,
                Contracts = ConvertInteractionToContract(pactSpecification.Interactions)
            };
        }

        private static IEnumerable<Contract> ConvertInteractionToContract(IEnumerable<Interaction> interactions)
        {
            return interactions.Select(interaction => new Contract
            {
                Name = interaction.Description,
                Request = ContractRequest.FromPactInteractionRequest(interaction.Request),
                Response = ContractResponse.FromPactInteractionResponse(interaction.Response)
            }).ToList();
        }
    }
}