using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace ConcordNet
{
    public class MockProviderService : IDisposable, IMockProviderService
    {
        private readonly IHost _host;
        public int Port = 4001;
        public string BaseAddress => $"http://localhost:{Port}/";


        private IDictionary<Contract, int> _contracts { get; set; }
        private Contract _buildingContract;
        private List<HttpRequest> _unmatchedRequests { get; }

        public MockProviderService(int port)
        {
            Port = port;

            _host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls(BaseAddress);
                webBuilder.Configure((WebHostBuilderContext context, IApplicationBuilder app) =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints => { endpoints.Map("/{**slug}", RequestHandler); });
                });
            }).Build();
            _host.RunAsync();
            _buildingContract = new Contract();
            _contracts = new Dictionary<Contract, int>();
            _unmatchedRequests = new List<HttpRequest>();
        }

        public IMockProviderService Given(string name)
        {
            if (!string.IsNullOrEmpty(_buildingContract.Name))
            {
                throw new Exception($"Finish contract with WillRespondWith on {_buildingContract.Name}");
            }

            _buildingContract.Name = name;
            return this;
        }

        public IMockProviderService UponReceiving(string scenario)
        {
            if (!string.IsNullOrEmpty(_buildingContract.Scenario))
            {
                throw new Exception($"Finish contract with WillRespondWith on {_buildingContract.Scenario}");
            }

            _buildingContract.Scenario = scenario;
            return this;
        }

        public IMockProviderService With(ContractRequest request)
        {
            if (_buildingContract.Request != null)
            {
                throw new Exception($"Finish contract with WillRespondWith on {_buildingContract.Request.Url}");
            }

            _buildingContract.Request = request;
            return this;
        }

        public void WillRespondWith(ContractResponse response)
        {
            if (_buildingContract.Request == null)
            {
                throw new Exception("Request not defined for response");
            }

            _buildingContract.Response = response;

            _contracts.Add(_buildingContract, 0);
            _buildingContract = new Contract();
        }


        private async Task RequestHandler(HttpContext context)
        {
            var contract = FindContract(context);
            if (contract == null)
            {
                context.Response.StatusCode = 404;
                _unmatchedRequests.Add(context.Request);
                await context.Response.CompleteAsync();
            }
            else
            {
                context.Response.StatusCode = (int) contract.Response.StatusCode;

                if (contract.Response.Headers != null)
                {
                    foreach (var header in contract.Response.Headers)
                    {
                        context.Response.Headers.Add(header.Key, header.Value);
                    }
                }

                if (contract.Response.Body == null)
                {
                    await context.Response.CompleteAsync();
                }

                await context.Response.WriteAsync(JsonConvert.SerializeObject(contract.Response.Body));

                _contracts[contract]++;
            }
        }

        private Contract FindContract(HttpContext context)
        {
            return _contracts.Keys.FirstOrDefault(c => ContractMatches(c.Request, context.Request));
        }

        private bool ContractMatches(ContractRequest contract, HttpRequest request)
        {
            return contract.Method == request.Method && contract.Url == request.Path;
        }

        public void Dispose()
        {
            _host.StopAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<Contract> GetContracts()
        {
            return _contracts.Keys;
        }

        public bool HasUnverifiedContracts => UnverifiedContracts.Count > 0;

        public IReadOnlyList<Contract> UnverifiedContracts =>
            _contracts.Where(c => c.Value == 0)?.ToDictionary(c => c.Key, c => c.Value).Keys.ToList();

        public IReadOnlyList<HttpRequest> UnmatchedRequests => _unmatchedRequests;
    }
}