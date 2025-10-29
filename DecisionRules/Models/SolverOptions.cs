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

        /// <summary>
        /// Default constructor for deserialization.
        /// </summary>
        public SolverOptions() { }

        /// <summary>
        /// Creates a new instance of SolverOptions.
        /// </summary>
        public SolverOptions(bool? debug, string? corrId, bool? audit, int? auditTtl)
        {
            Debug = debug;
            CorrId = corrId;
            Audit = audit;
            AuditTtl = auditTtl;
        }
    }
}