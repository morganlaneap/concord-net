using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConcordNet
{
    public class ContractDefinitionVerifier
    {
        private readonly HttpClient _httpClient;

        public IScenarioHandler ScenarioHandler { get; set; }

        public ContractDefinitionVerifier(int testServerPort)
        {
            _httpClient = new HttpClient {BaseAddress = new Uri($"http://localhost:{testServerPort}")};
        }

        public async Task Verify(ContractDefinition contractDefinition)
        {
            foreach (var contract in contractDefinition.Contracts)
            {
                // Setup our scenario first
                ScenarioHandler?.RunScenario(contract.Scenario);

                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_httpClient.BaseAddress, contract.Request.Url.Remove(0, 1))
                };
                
                var rawResponse = await _httpClient.SendAsync(httpRequest);
                
                Assert.That(rawResponse.StatusCode, Is.EqualTo(contract.Response.StatusCode));

                if (contract.Response.Body != null)
                {
                    var rawResponseBody = JsonConvert.DeserializeObject(await rawResponse.Content.ReadAsStringAsync());
                    Assert.That(rawResponseBody, Is.EqualTo(contract.Response.Body));
                }
            }
        }
    }
}