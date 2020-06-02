namespace ConcordNet.Interfaces
{
    public interface IMockProviderServiceFactory
    {
        public IMockProviderService GetMockProviderService(int port);
    }
}