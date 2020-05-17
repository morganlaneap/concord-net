using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class MockProviderTests
    {
        [Test]
        public async Task GivenAPort_MockProviderHosts()
        {
            int port = 4025;
            var mockProvider = new MockProviderService(port);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:{port}/");
            Assert.That(response.StatusCode == HttpStatusCode.Found);
        }
    }
}