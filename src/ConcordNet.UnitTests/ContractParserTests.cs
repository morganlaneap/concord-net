using System.Linq;
using ConcordNet.Models;
using NUnit.Framework;

namespace ConcordNet.UnitTests
{
    public class ContractParserTests
    {
        [Test]
        public void GivenValidFilePath_AndValidContent_ThenFileIsParsed()
        {
            var contractParser = new ContractParser();
            var contractDefinition = contractParser.ParseFile("./TestFiles/website-api.json");
            
            Assert.That(contractDefinition, Is.Not.Null);
            Assert.That(contractDefinition.Consumer, Is.EqualTo("website"));
            Assert.That(contractDefinition.Provider, Is.EqualTo("api"));
            Assert.That(contractDefinition.Contracts.Count(), Is.Not.EqualTo(0));
        } 
        
        [Test]
        public void GivenValidFilePath_WhenFileIsPactSpecification_AndValidContent_ThenFileIsParsed()
        {
            var contractParser = new ContractParser();
            var contractDefinition = contractParser.ParseFile("./TestFiles/pact-specification.json", true);
            
            Assert.That(contractDefinition, Is.Not.Null);
            Assert.That(contractDefinition.Consumer, Is.EqualTo("website"));
            Assert.That(contractDefinition.Provider, Is.EqualTo("api"));
            Assert.That(contractDefinition.Contracts.Count(), Is.EqualTo(1));
        } 
    }
}