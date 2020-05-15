using System.Collections.Generic;
using ExampleApi.Data;
using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("/data")]
    public class DataController : Controller
    {
        private readonly DataProvider _dataProvider;
        
        public DataController(DataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok(_dataProvider.GetData());
        }
    }
}