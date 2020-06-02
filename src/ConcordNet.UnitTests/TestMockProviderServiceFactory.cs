using System.Collections.Generic;
using ConcordNet.Interfaces;
using ConcordNet.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ConcordNet.UnitTests
{
    public class TestMockProviderServiceFactory : IMockProviderServiceFactory
    {
        public Mock<IMockProviderService> mockedMockProviderService { get; set; }

        public TestMockProviderServiceFactory()
        {
            mockedMockProviderService = new Mock<IMockProviderService>();
            mockedMockProviderService.Setup(mps => mps.Given(It.IsAny<string>()))
                .Returns(mockedMockProviderService.Object);
            mockedMockProviderService.Setup(mps => mps.With(It.IsAny<ContractRequest>()))
                .Returns(mockedMockProviderService.Object);
            mockedMockProviderService.Setup(mps => mps.UponReceiving(It.IsAny<string>()))
                .Returns(mockedMockProviderService.Object);
            mockedMockProviderService.Setup(mps => mps.UnverifiedContracts).Returns(new List<Contract>());
            mockedMockProviderService.Setup(mps => mps.UnmatchedRequests).Returns(new List<HttpRequest>());
        }
        public IMockProviderService GetMockProviderService(int port)
        {
            return mockedMockProviderService.Object;
        }
    }
}