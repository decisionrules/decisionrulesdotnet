using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class Context
    {

        [JsonPropertyName("billingUserId")]
        public string? BillingUserId { get; set; }

        [JsonPropertyName("environmentGroup")]
        public string? EnvironmentGroup { get; set; }

        // Matches the "nullable" comment in the Java code
        [JsonPropertyName("executorUser")]
        public string? ExecutorUser { get; set; }

        [JsonPropertyName("parallelLimit")]
        public int ParallelLimit { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }
        [JsonPropertyName("spaceId")]
        public string? SpaceId { get; set; }

        [JsonPropertyName("usedApiKeyId")]
        public string? UsedApiKeyId { get; set; }
    }
}