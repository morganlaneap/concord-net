using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ConcordNet.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class GeneratorTests
    {
        
        [Test]
        public void GivenATest_TheContractGeneratorCreatesAFile()
        {
            var tempDir = $"../../../pacts/{Guid.NewGuid().ToString()}";
            var config = new ConcordGeneratorConfig()
            {
                ContractDirectory = tempDir
            };
            var gen = new ConcordGenerator(config);
            gen.ServiceConsumer("consumer1").HasPactWith("provider1");
            var mockSvc = gen.MockService(4099);
            mockSvc.Given("A Test").UponReceiving("Some Scenario").With(new ContractRequest(){Url = "/abcd",Method = "GET"}).WillRespondWith(new ContractResponse(){StatusCode = HttpStatusCode.OK});
            gen.Generate();

            var lines = File.ReadAllText(tempDir+"/consumer1-provider1.json");
            var obj = JsonConvert.DeserializeObject<List<Contract>>(lines);
            Assert.That(obj.GetType() == typeof(List<Contract>));
            
            CleanUpDirectory(tempDir);
            
        }

        private void CleanUpDirectory(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            Directory.Delete(path);
            
        }
    }
}