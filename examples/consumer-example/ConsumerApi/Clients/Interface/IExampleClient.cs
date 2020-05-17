using System.Collections.Generic;
using System.Threading.Tasks;
using ConsumerApi.Clients.Model;

namespace ConsumerApi.Clients.Interface
{
    public interface IExampleClient
    {
        public Task<IEnumerable<ExampleData>> GetSomethingFromApi();
    }
}