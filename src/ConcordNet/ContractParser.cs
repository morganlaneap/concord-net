using System;
using System.IO;
using ConcordNet.Models;
using ConcordNet.Models.Pact;
using Newtonsoft.Json;

namespace ConcordNet
{
    public class ContractParser
    {
        public ContractDefinition ParseFile(string filePath, bool isPactFile = false)
        {
            try
            {
                var fileContents = File.ReadAllText(filePath);

                if (!isPactFile) return JsonConvert.DeserializeObject<ContractDefinition>(fileContents);
                
                var pactModel = JsonConvert.DeserializeObject<PactSpecification>(fileContents);
                return ContractDefinition.FromPactSpecification(pactModel);
            }
            catch (Exception exception)
            {
                // TODO: handle and log exception
                return null;
            }
        }
    }
}