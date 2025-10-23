using System.Collections.Generic;
using System.Text.Json.Serialization;
using DecisionRules.Enums; // Assuming FolderType is in this namespace

namespace DecisionRules.Models
{
    public class FolderData
    {
        // This attribute serializes the enum as a string (e.g., "FOLDER") 
        // instead of its number (e.g., 0).
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("type")]
        // This replaces @JsonInclude(JsonInclude.Include.NON_NULL)
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FolderType? Type { get; set; }

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

        [JsonPropertyName("version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Version { get; set; }

        [JsonPropertyName("children")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<FolderData>? Children { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public FolderData() { }

        /// <summary>
        /// Full constructor.
        /// </summary>
        public FolderData(string? baseId, List<FolderData>? children, string? id, string? name, FolderType? type, int? version, string? ruleAlias)
        {
            BaseId = baseId;
            Children = children;
            Id = id;
            Name = name;
            Type = type;
            Version = version;
            RuleAlias = ruleAlias;
        }

        /// <summary>
        /// A direct translation of the Java Builder pattern.
        /// </summary>
        public class Builder
        {
            private FolderType? _type;
            private string? _name;
            private List<FolderData>? _children;

            public Builder SetType(FolderType type)
            {
                _type = type;
                return this;
            }

            public Builder SetName(string name)
            {
                _name = name;
                return this;
            }

            public Builder SetChildren(List<FolderData> children)
            {
                _children = children;
                return this;
            }

            public FolderData Build()
            {
                // This matches the Java builder's constructor call
                return new FolderData(
                    baseId: null,
                    children: _children,
                    id: null,
                    name: _name,
                    type: _type,
                    version: null,
                    ruleAlias: null
                );
            }
        }
    }
}