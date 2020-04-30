using System.Collections.Generic;
using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("/data")]
    public class DataController : Controller
    {
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(new List<ExampleData>
            {
                new ExampleData {Id = "123", Color = "Red"},
                new ExampleData {Id = "789", Color = "Blue"}
            });
        }
    }
}