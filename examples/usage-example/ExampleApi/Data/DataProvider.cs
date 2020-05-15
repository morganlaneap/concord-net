using System.Collections.Generic;
using ExampleApi.Models;

namespace ExampleApi.Data
{
    public class DataProvider
    {
        public virtual IEnumerable<ExampleData> GetData()
        {
            return new[]
            {
                new ExampleData {Id = "123", Color = "Red"},
                new ExampleData {Id = "789", Color = "Blue"}
            };
        }
    }
}