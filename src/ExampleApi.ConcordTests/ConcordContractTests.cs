using ConcordNet;
using NUnit.Framework;

namespace ExampleApi.ConcordTests
{
    public class ConcordContractTests
    {
        [Test]
        public void Verify()
        {
            var concordHost = new ConcordHost();
            concordHost.RegisterTestServer<Startup>();
            concordHost.AddContractDefinition(@"E:\Development\concord-net\examples\contracts\website-api.json");
            concordHost.VerifyContractDefinitions();
        }
    }
}