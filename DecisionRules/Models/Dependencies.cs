using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class Dependencies
    {
        [JsonPropertyName("rule")]
        public Rule? Rule { get; set; }

        [JsonPropertyName("dependencies")]
        public List<object>? DependenciesList { get; set; }
    }
}