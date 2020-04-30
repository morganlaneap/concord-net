using ConcordNet.UnitTests.TestClasses;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class ConcordHostTests
    {
        [Test]
        public void GivenValidSetup_WhenCreatingNewHost_ThenHostIsCreated()
        {
            Assert.DoesNotThrow(() =>
            {
                var concordHost = new ConcordHost();
                concordHost.RegisterTestServer<TestStartup>();
                Assert.NotNull(concordHost.TestServer);
            });
        }

        [Test]
        public void GivenValidFile_WhenAddingAsContractDefinition_ThenFileIsAddedToList()
        {
            Assert.DoesNotThrow(() =>
            {
                var concordHost = new ConcordHost();
                concordHost.AddContractDefinition("./TestFiles/website-api.json");
                Assert.That(concordHost.ContractDefinitions.Count, Is.EqualTo(1));
            });
        }
    }
}