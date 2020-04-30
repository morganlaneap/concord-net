using System;
using System.IO;
using ConcordNet.Models;
using Newtonsoft.Json;

namespace ConcordNet
{
    public class ContractParser
    {
        public ContractDefinition ParseFile(string filePath)
        {
            try
            {
                var fileContents = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ContractDefinition>(fileContents);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}