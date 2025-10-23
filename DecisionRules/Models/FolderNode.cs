using System.Text.Json.Serialization;
using DecisionRules.Enums; // Assuming FolderType is in this namespace

namespace DecisionRules.Models
{
    public class FolderNode
    {
        // This attribute replaces @JsonInclude(JsonInclude.Include.NON_NULL) for this property
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        // This attribute replaces @JsonInclude(JsonInclude.Include.NON_NULL) for this property
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        // Converts the enum to its string name (e.g., "FOLDER") during serialization
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("type")]
        public FolderType? Type { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public FolderNode() { }

        /// <summary>
        /// Creates a new instance of FolderNode.
        /// </summary>
        public FolderNode(string? id, FolderType? type)
        {
            Id = id;
            Type = type;
        }
    }
}