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

        [JsonPropertyName("lookupMethod")]
        public LookupMethodOptions? LookupMethod { get; set; }

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public SolverOptions() { }

        /// <summary>
        /// Creates a new instance of SolverOptions.
        /// </summary>
        public SolverOptions(
            bool? debug = null,
            string? corrId = null,
            bool? audit = null,
            int? auditTtl = null,
            string? aliasConflictPath = null,
            StrategyOptions? strategy = null,
            ColsOptions? cols = null,
            LookupMethodOptions? lookupMethod = null)
        {
            Debug = debug;
            CorrId = corrId;
            Audit = audit;
            AuditTtl = auditTtl;
            AliasConflictPath = aliasConflictPath;
            Strategy = strategy;
            LookupMethod = lookupMethod;
            Cols = cols;
        }

        /// <summary>
        /// Creates a new builder for constructing SolverOptions.
        /// </summary>
        public static Builder CreateBuilder() => new Builder();

        /// <summary>
        /// Builder class for creating SolverOptions instances.
        /// </summary>
        public class Builder
        {
            private bool? _debug;
            private string? _corrId;
            private bool? _audit;
            private int? _auditTtl;
            private string? _aliasConflictPath;
            private StrategyOptions? _strategy;
            private ColsOptions? _cols;
            private LookupMethodOptions? _lookupMethod;

            /// <summary>
            /// Sets the debug flag.
            /// </summary>
            public Builder WithDebug(bool debug)
            {
                _debug = debug;
                return this;
            }

            /// <summary>
            /// Sets the correlation ID.
            /// </summary>
            public Builder WithCorrId(string corrId)
            {
                _corrId = corrId;
                return this;
            }

            /// <summary>
            /// Sets the audit flag.
            /// </summary>
            public Builder WithAudit(bool audit)
            {
                _audit = audit;
                return this;
            }

            /// <summary>
            /// Sets the audit TTL (time to live).
            /// </summary>
            public Builder WithAuditTtl(int auditTtl)
            {
                _auditTtl = auditTtl;
                return this;
            }

            /// <summary>
            /// Sets the alias conflict path.
            /// </summary>
            public Builder WithAliasConflictPath(string aliasConflictPath)
            {
                _aliasConflictPath = aliasConflictPath;
                return this;
            }

            /// <summary>
            /// Sets the strategy options.
            /// </summary>
            public Builder WithStrategy(StrategyOptions strategy)
            {
                _strategy = strategy;
                return this;
            }

            /// <summary>
            /// Sets the column options.
            /// </summary>
            public Builder WithCols(ColsOptions cols)
            {
                _cols = cols;
                return this;
            }

            /// <summary>
            /// Sets the lookup method.
            /// </summary>
            public Builder WithLookupMethod(LookupMethodOptions lookupMethod)
            {
                _lookupMethod = lookupMethod;
                return this;
            }

            /// <summary>
            /// Builds and returns the SolverOptions instance.
            /// </summary>
            public SolverOptions Build()
            {
                return new SolverOptions(
                    _debug,
                    _corrId,
                    _audit,
                    _auditTtl,
                    _aliasConflictPath,
                    _strategy,
                    _cols,
                    _lookupMethod
                );
            }
        }
    }

    public class ColsOptions
    {
        [JsonPropertyName("includedConditionCols")]
        public List<string>? IncludedConditionCols { get; set; }

        [JsonPropertyName("excludedConditionCols")]
        public List<string>? ExcludedConditionCols { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColsOptions() { }

        /// <summary>
        /// Creates a new instance of ColsOptions.
        /// </summary>
        public ColsOptions(List<string>? includedConditionCols = null, List<string>? excludedConditionCols = null)
        {
            IncludedConditionCols = includedConditionCols;
            ExcludedConditionCols = excludedConditionCols;
        }

        /// <summary>
        /// Creates a new builder for constructing ColsOptions.
        /// </summary>
        public static Builder CreateBuilder() => new Builder();

        /// <summary>
        /// Builder class for creating ColsOptions instances.
        /// </summary>
        public class Builder
        {
            private List<string>? _includedConditionCols;
            private List<string>? _excludedConditionCols;

            /// <summary>
            /// Sets the included condition columns.
            /// </summary>
            public Builder WithIncludedConditionCols(List<string> includedConditionCols)
            {
                _includedConditionCols = includedConditionCols;
                return this;
            }

            /// <summary>
            /// Adds a single included condition column.
            /// </summary>
            public Builder AddIncludedConditionCol(string column)
            {
                _includedConditionCols ??= new List<string>();
                _includedConditionCols.Add(column);
                return this;
            }

            /// <summary>
            /// Sets the excluded condition columns.
            /// </summary>
            public Builder WithExcludedConditionCols(List<string> excludedConditionCols)
            {
                _excludedConditionCols = excludedConditionCols;
                return this;
            }

            /// <summary>
            /// Adds a single excluded condition column.
            /// </summary>
            public Builder AddExcludedConditionCol(string column)
            {
                _excludedConditionCols ??= new List<string>();
                _excludedConditionCols.Add(column);
                return this;
            }

            /// <summary>
            /// Builds and returns the ColsOptions instance.
            /// </summary>
            public ColsOptions Build()
            {
                return new ColsOptions(_includedConditionCols, _excludedConditionCols);
            }
        }
    }
}