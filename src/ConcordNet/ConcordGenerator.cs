using System;
using System.IO;
using System.Text.Json;
using ConcordNet.Interfaces;

namespace ConcordNet
{
    public class ConcordGeneratorConfig
    {
        public string ContractDirectory { get; set; }
    }
    public class ConcordGenerator : IConcordGenerator
    {
        private string contractDirectory;
        
        private string consumer;
        private string provider;
        
        private IMockProviderService _providerService;


        public ConcordGenerator(ConcordGeneratorConfig config)
        {
            contractDirectory = config.ContractDirectory;
            if (!contractDirectory.EndsWith("/"))
            {
                contractDirectory += "/";
            }

            if (!Directory.Exists(contractDirectory))
            {
                Directory.CreateDirectory(contractDirectory);
            }
        }
        public IMockProviderService MockService(int port)
        {
            _providerService = new MockProviderService(port);
            return _providerService;
        }

        public IConcordGenerator ServiceConsumer(string ServiceConsumer)
        {
            consumer = ServiceConsumer;
            return this;
        }

        public IConcordGenerator HasContractWith(string providerService)
        {
            provider = providerService;
            return this;
        }


        public void Generate()
        {
            if (_providerService == null)
            {
                throw new Exception("Mock the provider service");
            }

            if (string.IsNullOrEmpty(consumer))
            {
                throw new Exception("Define the service");
            }

            if (string.IsNullOrEmpty(provider))
            {
                throw new Exception("Define the provider");
            }

            var contracts = _providerService.GetContracts();
            File.WriteAllText($"{contractDirectory}{consumer}-{provider}.json", JsonSerializer.Serialize(contracts));
        }
    }
}