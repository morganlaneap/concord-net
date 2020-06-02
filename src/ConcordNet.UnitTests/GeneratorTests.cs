﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ConcordNet.Exceptions;
using ConcordNet.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class GeneratorTests
    {
        [Test]
        public void GivenATest_AndTheProviderIsCalled_TheContractGeneratorCreatesAFile()
        {
            var tempDir = $"../../../pacts/{Guid.NewGuid().ToString()}";
            var config = new ConcordGeneratorConfig()
            {
                ContractDirectory = tempDir
            };
            var mockProviderServiceFactory = new TestMockProviderServiceFactory();
            mockProviderServiceFactory.mockedMockProviderService
                .Setup(c => c.HasUnverifiedContracts).Returns(false);
            var gen = new ConcordGenerator(config, mockProviderServiceFactory);

            gen.ServiceConsumer("consumer1").HasContractWith("provider1");
            var mockSvc = gen.MockService(43173);
            mockSvc.Given("A Test").UponReceiving("Some Scenario")
                .With(new ContractRequest() {Url = "/abcd", Method = "GET"}).WillRespondWith(
                    new ContractResponse()
                    {
                        StatusCode = HttpStatusCode.OK
                    });
            gen.Generate();

            var lines = File.ReadAllText(tempDir + "/consumer1-provider1.json");
            var obj = JsonConvert.DeserializeObject<ContractDefinition>(lines);
            Assert.That(obj.GetType() == typeof(ContractDefinition));

            CleanUpDirectory(tempDir);
        }

        [Test]
        public void GivenATest_AndWeDontCallTheProvider_TheFileIsNotCreated()
        {
            var tempDir = $"../../../pacts/{Guid.NewGuid().ToString()}";
            var config = new ConcordGeneratorConfig()
            {
                ContractDirectory = tempDir
            };
            var mockProviderServiceFactory = new TestMockProviderServiceFactory();
            mockProviderServiceFactory.mockedMockProviderService
                .Setup(c => c.HasUnverifiedContracts).Returns(true);
            mockProviderServiceFactory.mockedMockProviderService
                .Setup(c => c.UnverifiedContracts).Returns(new List<Contract> {new Contract() { }});
            var gen = new ConcordGenerator(config, mockProviderServiceFactory);
            gen.ServiceConsumer("consumer1").HasContractWith("provider1");
            var mockSvc = gen.MockService(43173);
            mockSvc.Given("A Test").UponReceiving("Some Scenario")
                .With(new ContractRequest() {Url = "/abcd", Method = "GET"})
                .WillRespondWith(new ContractResponse() {StatusCode = HttpStatusCode.OK});

            
            Assert.Throws<UnverifiedContractsException>(() => gen.Generate());
            Assert.That(File.Exists(tempDir + "/consumer1-provider1.json") == false);

            CleanUpDirectory(tempDir);
        }

        private void CleanUpDirectory(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }

            Directory.Delete(path);
        }
    }
}