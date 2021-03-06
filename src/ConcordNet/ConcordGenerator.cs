﻿using System;
using System.IO;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            };
            
            
            var contracts = _providerService.GetContracts();
            var contractDefinition = new ContractDefinition()
            {
                Provider = provider,
                Consumer = consumer,
                Contracts = contracts
            };
            File.WriteAllText($"{contractDirectory}{consumer}-{provider}.json",
                JsonConvert.SerializeObject(contractDefinition,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = contractResolver,
                        Formatting = Formatting.Indented
                    }));
        }
    }
}