using System.Text.Json.Serialization;

namespace ConsumerApi.Clients.Model
{
    public class ExampleData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("color")]
        public string Color { get; set; }
    }
}