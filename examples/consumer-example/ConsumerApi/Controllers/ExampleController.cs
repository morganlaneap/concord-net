using System.Collections.Generic;
using System.Threading.Tasks;
using ConsumerApi.Clients.Interface;
using ConsumerApi.Clients.Model;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExampleController :ControllerBase
    {
        private readonly IExampleClient _client;

        public ExampleController(IExampleClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _client.GetSomethingFromApi());
        }
    }
}