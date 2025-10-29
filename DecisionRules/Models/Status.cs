using System.Text.Json.Serialization;

namespace DecisionRules.Models
{

    public class Status
    {
        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
    
}