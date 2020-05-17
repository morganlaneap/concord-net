using System.Collections.Generic;
using ConcordNet;
using ConcordNet.Interfaces;
using ExampleApi.Data;
using ExampleApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace ExampleApi.ConcordTests
{
    public class ConcordContractTests
    {
        private readonly ScenarioHandler _scenarioHandler = new ScenarioHandler();
        
        [Test]
        public void VerifyContracts()
        {
            var concordHost = new ConcordHost();
            concordHost.RegisterTestServer<Startup>(ConfigureDependencyInjection, scenarioHandler: _scenarioHandler);
            concordHost.AddContractDefinition(@"../../../../../../src/ConcordNet.UnitTests/TestFiles/website-api.json");
            concordHost.VerifyContractDefinitions();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            var dataProviderMock = new Mock<DataProvider>();
            dataProviderMock.Setup(m => m.GetData()).Returns(() => _scenarioHandler.ExampleData);

            services.AddSingleton(dataProviderMock.Object);
        }
    }
}