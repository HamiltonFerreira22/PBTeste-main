using System.Text.Json.Serialization;

namespace PBTeste.Model.Response
{
   public class ExampleResponse
    {
        [JsonPropertyName("example")]
        public string example { get; set; }
    }
}
