using ConcordNet;
using NUnit.Framework;

namespace ExampleApi.ConcordTests
{
    public class ConcordContractTests
    {
        [Test]
        public void VerifyContracts()
        {
            var concordHost = new ConcordHost();
            concordHost.RegisterTestServer<Startup>();
            concordHost.AddContractDefinition(@"E:\Development\concord-net\src\ConcordNet.UnitTests\TestFiles\website-api.json");
            concordHost.VerifyContractDefinitions();
        }
    }
}