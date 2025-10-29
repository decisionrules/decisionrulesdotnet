using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class RuleOptions
    {
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public RuleOptions() { }

        /// <summary>
        /// Creates a new instance of RuleOptions.
        /// </summary>
        public RuleOptions(string? path, int? version)
        {
            Path = path;
            Version = version;
        }
    }
}