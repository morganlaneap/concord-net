namespace ConcordNet.Interfaces
{
    public interface IConcordGenerator
    {
        public IMockProviderService MockService(int port);

        public IConcordGenerator ServiceConsumer(string serviceConsumer);
        public IConcordGenerator HasContractWith(string providerService);
        public void Generate();
    }
}