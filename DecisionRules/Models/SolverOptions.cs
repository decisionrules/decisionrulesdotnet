using DecisionRules.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    public class SolverOptions
    {
        [JsonPropertyName("debug")]
        public bool? Debug { get; set; }

        [JsonPropertyName("corrId")]
        public string? CorrId { get; set; }

        [JsonPropertyName("audit")]
        public bool? Audit { get; set; }

        [JsonPropertyName("auditTtl")]
        public int? AuditTtl { get; set; }

        [JsonPropertyName("aliasConflictPath")]
        public string? AliasConflictPath { get; set; }

        [JsonPropertyName("strategy")]
        public StrategyOptions? Strategy { get; set; }

        [JsonPropertyName("cols")]
        public ColsOptions? Cols { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public SolverOptions() { }

        /// <summary>
        /// Creates a new instance of SolverOptions.
        /// </summary>
        public SolverOptions(
                 bool? debug,
                 string? corrId,
                 bool? audit,
                 int? auditTtl,
                 string? aliasConflictPath,
                 StrategyOptions? strategy,
                 ColsOptions? cols)
        {
            Debug = debug;
            CorrId = corrId;
            Audit = audit;
            AuditTtl = auditTtl;
            AliasConflictPath = aliasConflictPath;
            Strategy = strategy;
            Cols = cols;
        }
    }

    public class ColsOptions
    {
        [JsonPropertyName("includedConditionCols")]
        public List<string>? IncludedConditionCols { get; set; }

        [JsonPropertyName("excludedConditionCols")]
        public List<string>? ExcludedConditionCols { get; set; }
    }
}