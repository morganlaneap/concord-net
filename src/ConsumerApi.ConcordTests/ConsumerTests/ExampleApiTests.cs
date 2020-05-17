using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using ConsumerApi.Clients;
using ConsumerApi.Clients.Model;
using Moq;
using NUnit.Framework;

namespace ConsumerApi.ConcordTests.ConsumerTests
{
    [TestFixture]
    public class ExampleApiTests
    {
        private IMockProviderService _mockProviderService;
        private ExampleApiFixture _fixture;
        private IHttpClientFactory _factory;
        [OneTimeSetUp]
        public void Init()
        {
            _fixture = new ExampleApiFixture();
            _mockProviderService = _fixture.MockProviderService;
            var factory = new Mock<IHttpClientFactory>();
            factory.Setup(c => c.CreateClient("ExampleApi")).Returns(new HttpClient()
                {BaseAddress = new Uri(_mockProviderService.BaseAddress)});
            _factory = factory.Object;

        }

        [Test]
        public async  Task DoSomethingWithAClient()
        {
            _mockProviderService.Given("A Test").UponReceiving("Some State").With(new ContractRequest()
            {
                Url = "/data",
                Method = "GET"
            }).WillRespondWith(new ContractResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Body = new List<ExampleData>{new ExampleData{Color="blue",Id="5"}}
            });
            var client = new ExampleClient(_factory);
            var response = await client.GetSomethingFromApi();
            Assert.That(response.Count() == 1);
            Assert.That(response.First().Color == "blue");
            
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _fixture.Dispose();
        }
    }
}