using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class FolderOptions
    {
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public FolderOptions() { }

        /// <summary>
        /// Creates a new instance of FolderOptions.
        /// </summary>
        public FolderOptions(string? path)
        {
            Path = path;
        }
    }
}