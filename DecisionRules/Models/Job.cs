using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public partial class Job
    {
        [JsonPropertyName("ruleReference")]
        public RuleReference? RuleReference { get; set; }

        [JsonPropertyName("inputData")]
        public Dictionary<string, object>? InputData { get; set; }

        [JsonPropertyName("jobId")]
        public string? JobId { get; set; }

        [JsonPropertyName("context")]
        public Context? Context { get; set; }

        [JsonPropertyName("status")]
        public Status? Status { get; set; }

        [JsonPropertyName("correlationId")]
        public string? CorrelationId { get; set; }

        [JsonPropertyName("createdAt")]
        public string? CreatedAt { get; set; } // Or DateTime? if you prefer to parse it

        [JsonPropertyName("updatedAt")]
        public string? UpdatedAt { get; set; } // Or DateTime?
    }
}