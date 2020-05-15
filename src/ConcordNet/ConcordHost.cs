using System;
using System.Collections.Generic;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConcordNet
{
    public class ConcordHost
    {
        private readonly int _testSeverPort;
        
        private readonly ContractParser _contractParser;
        private readonly ContractDefinitionVerifier _contractDefinitionVerifier;
        
        public IHostBuilder TestServer { get; set; }

        public List<ContractDefinition> ContractDefinitions { get; set; }

        public ConcordHost(int testServerPort = 45678)
        {
            _testSeverPort = testServerPort;
            _contractParser = new ContractParser();
            _contractDefinitionVerifier = new ContractDefinitionVerifier(testServerPort);
            
            ContractDefinitions = new List<ContractDefinition>();
        }

        public void RegisterTestServer<TStartupClass>(
            Action<IServiceCollection> dependencyInjectionConfiguration = null,
            IConfiguration applicationConfiguration = null, IScenarioHandler scenarioHandler = null) where TStartupClass : class
        {
            TestServer = new HostBuilder().ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<TStartupClass>();
                builder.UseUrls($"http://0.0.0.0:{_testSeverPort}");

                if (dependencyInjectionConfiguration != null)
                {
                    builder.ConfigureServices(dependencyInjectionConfiguration);
                }

                if (applicationConfiguration != null)
                {
                    builder.UseConfiguration(applicationConfiguration);
                }
            });
            _contractDefinitionVerifier.ScenarioHandler = scenarioHandler;
        }

        public void AddContractDefinition(string filePath, bool isPactFile = false)
        {
            var contractDefinition = _contractParser.ParseFile(filePath, isPactFile);
            ContractDefinitions.Add(contractDefinition);
        }

        public void VerifyContractDefinitions()
        {
            using (TestServer.StartAsync().GetAwaiter().GetResult())
            {
                foreach (var contractDefinition in ContractDefinitions)
                {
                    _contractDefinitionVerifier.Verify(contractDefinition).GetAwaiter().GetResult();
                }
            }
        }
    }
}