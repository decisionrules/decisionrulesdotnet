using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DecisionRules.Models
{
    public class RuleReference
    {
        [JsonPropertyName("baseId")]
        public string? BaseId { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
