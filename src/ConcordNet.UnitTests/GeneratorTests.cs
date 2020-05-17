using System.Collections.Generic;
using System.IO;
using ConcordNet.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class GeneratorTests
    {
        [Test]
        public void GeneratorWritesValues()
        {
            var config = new ConcordGeneratorConfig()
            {
                ContractDirectory = "C:/temp/pacts2"
            };
            var gen = new ConcordGenerator(config);
            gen.ServiceConsumer("consumer1").HasPactWith("provider1");
            gen.MockService(4099);
            gen.Generate();

            var lines = File.ReadAllText("C:/temp/pacts2/consumer1-provider1.json");
            var obj = JsonConvert.DeserializeObject<List<Contract>>(lines);
            Assert.That(obj.GetType() == typeof(List<Contract>));
        }
    }
}