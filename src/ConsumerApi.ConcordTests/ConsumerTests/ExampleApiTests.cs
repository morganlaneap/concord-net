using ConcordNet.Interfaces;
using NUnit.Framework;

namespace ConsumerApi.ConcordTests.ConsumerTests
{
    [TestFixture]
    public class ExampleApiTests
    {
        private IMockProviderService _mockProviderService;
        private ExampleApiFixture _fixture;
        [OneTimeSetUp]
        public void Init()
        {
            _fixture = new ExampleApiFixture();
            _mockProviderService = _fixture.MockProviderService;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _fixture.Dispose();
        }
    }
}