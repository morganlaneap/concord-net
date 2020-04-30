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
                var testServer = concordHost.RegisterTestServer<TestStartup>();
                Assert.NotNull(testServer);
            });
        }
    }
}