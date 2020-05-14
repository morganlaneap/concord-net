using ConcordNet;
using ExampleApi.Data;
using ExampleApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace ExampleApi.ConcordTests
{
    public class ConcordContractTests
    {
        [Test]
        public void VerifyContracts()
        {
            var concordHost = new ConcordHost();
            concordHost.RegisterTestServer<Startup>(ConfigureDependencyInjection);
            concordHost.AddContractDefinition(@"../../../../ConcordNet.UnitTests/TestFiles/website-api.json");
            concordHost.VerifyContractDefinitions();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            var dataProviderMock = new Mock<DataProvider>();
            dataProviderMock.Setup(m => m.GetData()).Returns(new[]
            {
                new ExampleData
                {
                    Id = "ABC",
                    Color = "GREEN"
                }, new ExampleData
                {
                    Id = "JKG",
                    Color = "ORANGE"
                }
            });

            services.AddSingleton(dataProviderMock.Object);
        }
    }
}