using System.Collections.Generic;
using ConcordNet.Interfaces;
using ExampleApi.Models;

namespace ExampleApi.ConcordTests
{
    public class ScenarioHandler : IScenarioHandler
    {
        public readonly List<ExampleData> ExampleData = new List<ExampleData>();
        
        public void RunScenario(string scenarioName)
        {
            ExampleData.Clear();

            switch (scenarioName)
            {
                case "Data with id 123 and color GREEN":
                    ExampleData.Add(new ExampleData
                    {
                        Id = "123",
                        Color = "GREEN"
                    });
                    break;
                case "Data with id XYZ and color RED":
                    ExampleData.Add(new ExampleData
                    {
                        Id = "XYZ",
                        Color = "RED"
                    });
                    break;
                case "Data with id GGGGGG and color ORANGE":
                    ExampleData.Add(new ExampleData
                    {
                        Id = "GGGGGG",
                        Color = "ORANGE"
                    });
                    break;
            }
        }
    }
}