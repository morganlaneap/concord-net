using System;
using System.Dynamic;
using ConcordNet;
using ConcordNet.Interfaces;

namespace ConsumerApi.ConcordTests.ConsumerTests
{
    public class ExampleApiFixture : IDisposable
    {
        private IConcordGenerator ConcordGenerator { get; }
        
        public IMockProviderService MockProviderService { get; }

        private int port = 4035;

        public ExampleApiFixture()
        {
            var config = new ConcordGeneratorConfig {ContractDirectory = "C:/temp/pacts"};
            ConcordGenerator = new ConcordGenerator(config);
            ConcordGenerator.ServiceConsumer("ConsumerApi").HasPactWith("ExampleApi");
            MockProviderService = ConcordGenerator.MockService(port);
        }
        
        public void Dispose()
        {
            ConcordGenerator.Generate();
        }
    }
}