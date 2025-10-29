using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class FolderExport
    {
        [JsonPropertyName("export")]
        public Export? ExportData { get; set; } // Renamed to avoid conflict with class name

        public FolderExport() { }

        public FolderExport(Export? exportData)
        {
            ExportData = exportData;
        }

        // --- Nested Classes ---

        // Corresponds to Java's 'static class Export'
        public class Export
        {
            [JsonPropertyName("data")]
            public ExportFolderData? Data { get; set; }

            [JsonPropertyName("exportType")]
            public string? ExportType { get; set; }

            [JsonPropertyName("version")]
            public int? Version { get; set; } // Java 'Integer' -> C# 'int?'

            [JsonPropertyName("createdAt")]
            public DateTime? CreatedAt { get; set; } // Java 'Date' -> C# 'DateTime?'

            public Export() { }

            public Export(ExportFolderData? data, string? exportType, int? version, DateTime? createdAt)
            {
                Data = data;
                ExportType = exportType;
                Version = version;
                CreatedAt = createdAt;
            }
        }

        // Corresponds to Java's 'static class ExportFolderData'
        public class ExportFolderData
        {
            [JsonPropertyName("structure")]
            public FolderData? Structure { get; set; }

            [JsonPropertyName("rules")]
            public List<Rule>? Rules { get; set; } // Java 'List<Rule>' -> C# 'List<Rule>?'

            public ExportFolderData() { }

            public ExportFolderData(FolderData? structure, List<Rule>? rules)
            {
                Structure = structure;
                Rules = rules;
            }
        }
    }
}