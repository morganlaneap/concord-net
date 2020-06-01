using System.Collections.Generic;
using ConcordNet.Models;
using Microsoft.AspNetCore.Http;

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
        public bool HasUnverifiedContracts { get; }
        public IReadOnlyList<Contract> UnverifiedContracts { get; }
        public IReadOnlyList<HttpRequest> UnmatchedRequests { get;  }
    }
}