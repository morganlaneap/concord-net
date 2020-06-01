using ConcordNet.Interfaces;
using Moq;

namespace ConcordNet.UnitTests
{
    public class TestMockProviderServiceFactory : IMockProviderServiceFactory
    {
        public Mock<MockProviderService> mockedMockProviderService { get; set; }
        
        public IMockProviderService GetMockProviderService(int port)
        {
            return mockedMockProviderService.Object;
        }
    }
}