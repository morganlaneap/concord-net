using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ConsumerApi.Clients.Interface;
using ConsumerApi.Clients.Model;

namespace ConsumerApi.Clients
{
    public class ExampleClient : IExampleClient
    {
        private readonly HttpClient _exampleClient;
        
        public ExampleClient(IHttpClientFactory clientFactory)
        {
            _exampleClient = clientFactory.CreateClient("ExampleApi");
        }

        public async Task<IEnumerable<ExampleData>> GetSomethingFromApi()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/data");


            var response = await _exampleClient.SendAsync(request);
            var responseMessage = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<IEnumerable<ExampleData>>(responseMessage);
            }
            throw new Exception(responseMessage);
        }
    }
}