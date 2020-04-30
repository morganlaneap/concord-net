using ConcordNet.UnitTests.TestClasses;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class ConcordHostTests
    {
        [Test]
        public void ConstructingNewConcordHost_DoesNotThrowError()
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