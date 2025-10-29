using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class Duplicates
    {
        [JsonPropertyName("rule")]
        public Rule? Rule { get; set; }

        [JsonPropertyName("duplicates")]
        public List<object>? DuplicatesList { get; set; }
    }
}