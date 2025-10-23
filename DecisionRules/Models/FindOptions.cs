using System.Text.Json.Serialization;
using DecisionRules.Enums; // Assuming FolderType is in this namespace

namespace DecisionRules.Models
{
    // The [JsonIgnore] attribute on each property replaces the 
    // class-level @JsonInclude(JsonInclude.Include.NON_NULL)
    public class FindOptions
    {
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        [JsonPropertyName("baseId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BaseId { get; set; }

        [JsonPropertyName("ruleAlias")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleAlias { get; set; }

        [JsonPropertyName("ruleType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleType { get; set; }

        [JsonPropertyName("tags")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Tags { get; set; }

        [JsonPropertyName("ruleState")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleState { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(JsonStringEnumConverter))] // Serializes enum as string
        public FolderType? Type { get; set; }

        [JsonPropertyName("version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Version { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public FindOptions() { }

        /// <summary>
        /// Creates a new instance of FindOptions.
        /// </summary>
        public FindOptions(string? name, string? id, string? baseId, string? ruleAlias, string? ruleType, string[]? tags, string? ruleState, FolderType? type, int? version)
        {
            Name = name;
            Id = id;
            BaseId = baseId;
            RuleAlias = ruleAlias;
            RuleType = ruleType;
            Tags = tags;
            RuleState = ruleState;
            Type = type;
            Version = version;
        }
    }
}