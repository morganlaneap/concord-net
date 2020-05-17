﻿using System;
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
        
        private string Consumer;
        private string Provider;
        
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
            Consumer = ServiceConsumer;
            return this;
        }

        public IConcordGenerator HasContractWith(string providerService)
        {
            Provider = providerService;
            return this;
        }

        [Obsolete]
        public IConcordGenerator HasPactWith(string providerService)
        {
            Provider = providerService;
            return this;
        }

        public void Generate()
        {
            if (_providerService == null)
            {
                throw new Exception("Mock the provider service");
            }

            if (string.IsNullOrEmpty(Consumer))
            {
                throw new Exception("Define the service");
            }

            if (string.IsNullOrEmpty(Provider))
            {
                throw new Exception("Define the provider");
            }

            var contracts = _providerService.GetContracts();
            File.WriteAllText($"{contractDirectory}{Consumer}-{Provider}.json", JsonSerializer.Serialize(contracts));
        }
    }
}