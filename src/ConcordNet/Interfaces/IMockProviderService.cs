using System.Collections.Generic;
using ConcordNet.Models;

namespace ConcordNet.Interfaces
{
    public interface IMockProviderService
    {
        public string BaseAddress { get;  }
        public IEnumerable<Contract> GetContracts();

        public IMockProviderService Given(string name);
        public IMockProviderService UponReceiving(string scenario);

        public IMockProviderService With(ContractRequest request);

        public void WillRespondWith(ContractResponse response);
    }
}