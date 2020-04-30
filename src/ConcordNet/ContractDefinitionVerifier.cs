﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConcordNet.Models;
using NUnit.Framework;

namespace ConcordNet
{
    public class ContractDefinitionVerifier
    {
        private readonly HttpClient _httpClient;

        public ContractDefinitionVerifier(int testServerPort)
        {
            _httpClient = new HttpClient {BaseAddress = new Uri($"http://localhost:{testServerPort}")};
        }

        public async Task Verify(ContractDefinition contractDefinition)
        {
            foreach (var contract in contractDefinition.Contracts)
            {
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_httpClient.BaseAddress, contract.Request.Url.Remove(0, 1))
                };
                
                var rawResponse = await _httpClient.SendAsync(httpRequest);
                
                Assert.That(rawResponse.StatusCode, Is.EqualTo(contract.Response.StatusCode));
            }
        }
    }
}