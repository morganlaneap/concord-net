using System.Collections.Generic;
using ConcordNet.Models;

namespace ConcordNet.Interfaces
{
    public interface IMockProviderService
    {
        public IEnumerable<Contract> GetContracts();
    }
}