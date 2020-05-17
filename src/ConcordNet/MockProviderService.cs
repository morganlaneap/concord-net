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
using Contract = ConcordNet.Models.Contract;

namespace ConcordNet
{
    public class MockProviderService : IDisposable, IMockProviderService
    {
        private readonly IHost _host;
        public int Port = 4001;
        public string BaseAddress => $"http://localhost:{Port}/";
        
        private IList<Contract> contracts;

        private Contract buildingContract;

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
            buildingContract = new Contract();
            contracts = new List<Contract>();
        }

        public IMockProviderService Given(string name)
        {
            if (!string.IsNullOrEmpty(buildingContract.Name))
            {
                throw new Exception($"Finish contract with WillRespondWith on {buildingContract.Name}");
            }

            buildingContract.Name = name;
            return this;
        }

        public IMockProviderService UponReceiving(string scenario)
        {
            if (!string.IsNullOrEmpty(buildingContract.Scenario))
            {
                throw new Exception($"Finish contract with WillRespondWith on {buildingContract.Scenario}");
            }

            buildingContract.Scenario = scenario;
            return this;
        }

        public IMockProviderService With(ContractRequest request)
        {
            if (buildingContract.Request != null)
            {
                throw new Exception($"Finish contract with WillRespondWith on {buildingContract.Request.Url}");
            }

            buildingContract.Request = request;
            return this;
        }

        public void WillRespondWith(ContractResponse response)
        {
            if (buildingContract.Request == null)
            {
                throw new Exception("Request not defined for response");
            }
            buildingContract.Response = response;
            contracts.Add(buildingContract);
            buildingContract = new Contract();
        }


        private async Task RequestHandler(HttpContext context)
        {
            var contract = FindContract(context);
            if (contract == null)
            {
                context.Response.StatusCode = 404;
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
            }
        }

        private Contract FindContract(HttpContext context)
        {
            return contracts.FirstOrDefault(c => ContractMatches(c.Request, context.Request));
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
            return contracts;
        }
    }
}