using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConcordNet.Models;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class MockProviderTests
    {
        private int port = 4025;

        [Test]
        public async Task GivenAPort_MockProviderHosts()
        {
            var mockProvider = new MockProviderService(port);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:{port}/");
            Assert.That(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void GivenAContractToBuild_ItIsInTheStore()
        {
            var mockProvider = new MockProviderService(port);
            mockProvider.Given("A Test").UponReceiving("A Request").With(new ContractRequest()
            {
                Method = "Get",
                Url = "/1234"
            }).WillRespondWith(new ContractResponse()
            {
                StatusCode = HttpStatusCode.OK
            });
            var contractFromStore = mockProvider.GetContracts().FirstOrDefault(c => c.Name == "A Test");
            Assert.NotNull(contractFromStore);
            Assert.AreEqual("A Test", contractFromStore.Name);
            Assert.AreEqual("A Request", contractFromStore.Scenario);
            Assert.AreEqual("Get", contractFromStore.Request.Method);
            Assert.AreEqual("/1234", contractFromStore.Request.Url);
            Assert.AreEqual(HttpStatusCode.OK, contractFromStore.Response.StatusCode);
        }

        [Test]
        public void GivenAPartialContract_AndAttemptToStartAnother_ItThrowsException()
        {
            var mockProvider = new MockProviderService(port);
            mockProvider.Given("Test1");

            Assert.Throws<Exception>(() => mockProvider.Given("Test2"));

            mockProvider.UponReceiving("A Request");

            Assert.Throws<Exception>(() => mockProvider.Given("A Request 2"));
        }

        [Test]
        public void GivenAContractWithoutRequest_AndAttemptToComplete_ItThrowsException()
        {
            var mockProvider = new MockProviderService(port);
            mockProvider.Given("A Test");


            Assert.Throws<Exception>(() =>
                mockProvider.WillRespondWith(new ContractResponse() {StatusCode = HttpStatusCode.Accepted}));
        }

        [Test]
        public async Task GivenAContract_AndItsRequested_TheProviderReturnsTheResponse()
        {
            var mockProvider = new MockProviderService(port);
            mockProvider.Given("A Test").UponReceiving("A Scenario").With(new ContractRequest(){Url = "/1234",Method = "GET"}).WillRespondWith(new ContractResponse()
            {
                StatusCode = HttpStatusCode.OK
            });
            
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:{port}/1234");
            Assert.That(response.StatusCode == HttpStatusCode.OK);
            
            
        }
    }
}