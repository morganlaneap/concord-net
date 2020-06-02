using ConcordNet.Interfaces;

namespace ConcordNet
{
    public class DefaultMockProviderServiceFactory : IMockProviderServiceFactory
    {
        public IMockProviderService GetMockProviderService(int port) => new MockProviderService(port);

    }
}