using System;
using System.Dynamic;
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
            MockProviderService = ConcordGenerator.MockService(port);
        }
        
        public void Dispose()
        {
            ConcordGenerator.Generate();
        }
    }
}