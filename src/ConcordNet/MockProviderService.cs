using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ConcordNet
{
    public class MockProviderService : IDisposable,IMockProviderService
    {
        private readonly IHost _host;
        public int Port = 4001;
        public string BaseUrl => $"http://localhost:{Port}/";
        private IEnumerable<Contract> contracts;

        public MockProviderService(int port)
        {
            Port = port;

            _host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseUrls(BaseUrl);
                webBuilder.Configure((WebHostBuilderContext context, IApplicationBuilder app) =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.Map("/", RequestHandler);
                    });
                });
            }).Build();
            _host.RunAsync();
            contracts = new List<Contract>(){new Contract(){Name="NAme",Scenario = "Scenario1",Request = new ContractRequest(){Method = "GET",Url="/abcd"},Response = new ContractResponse(){StatusCode = HttpStatusCode.OK}}};
        }

        

        private async Task RequestHandler(HttpContext context)
        {
            var contract = FindContract(context);
            if (contract == null)
            {
                context.Response.StatusCode = 404;
                await context.Response.CompleteAsync();
            }
        }
        
        private Contract FindContract(HttpContext context)
        {
            return null;
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