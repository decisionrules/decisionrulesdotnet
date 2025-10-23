using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DecisionRules.Models
{
    // In System.Text.Json, ignoring unknown properties is the default
    // behavior, so @JsonIgnoreProperties(ignoreUnknown = true) is not needed.
    public class Rule
    {
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonPropertyName("inputSchema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? InputSchema { get; set; }

        [JsonPropertyName("outputSchema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? OutputSchema { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; } // int is non-nullable, matches Java's 'int'

        [JsonPropertyName("lastUpdate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? LastUpdate { get; set; }

        [JsonPropertyName("createdIn")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? CreatedIn { get; set; }

        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Status { get; set; }

        [JsonPropertyName("baseId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BaseId { get; set; }

        [JsonPropertyName("ruleId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleId { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }

        [JsonPropertyName("tags")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Tags { get; set; }

        [JsonPropertyName("auditLog")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? AuditLog { get; set; }

        [JsonPropertyName("ruleAlias")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleAlias { get; set; }

        [JsonPropertyName("locked")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Locked { get; set; }

        [JsonPropertyName("ruleAliasInfo")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleAliasInfo { get; set; }

        [JsonPropertyName("sessionId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SessionId { get; set; }

        [JsonPropertyName("decisionTable")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, object>? DecisionTable { get; set; }

        [JsonPropertyName("visualEditorData")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? VisualEditorData { get; set; }

        [JsonPropertyName("compositionId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CompositionId { get; set; }

        [JsonPropertyName("dataTree")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? DataTree { get; set; }

        [JsonPropertyName("rules")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<object>? Rules { get; set; }

        [JsonPropertyName("nodes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<object>? Nodes { get; set; }

        [JsonPropertyName("userVariables")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<object>? UserVariables { get; set; }

        [JsonPropertyName("previousBaseId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PreviousBaseId { get; set; }

        [JsonPropertyName("script")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Script { get; set; }

        [JsonPropertyName("selectedWebhookAliases")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? SelectedWebhookAliases { get; set; }

        [JsonPropertyName("workflowData")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? WorkflowData { get; set; }

        /// <summary>
        /// Default constructor for deserialization
        /// </summary>
        public Rule() { }

        /// <summary>
        /// Full constructor
        /// </summary>
        public Rule(string? name, string? description, object? inputSchema, object? outputSchema, int version, DateTime? lastUpdate, DateTime? createdIn, string? status, string? baseId, string? ruleId, string? type, List<string>? tags, Dictionary<string, object>? auditLog, string? ruleAlias, bool? locked, string? ruleAliasInfo, string? sessionId, Dictionary<string, object>? decisionTable, object? visualEditorData, string? compositionId, object? dataTree, List<object>? rules, List<object>? nodes, List<object>? userVariables, string? previousBaseId, object? script, List<string>? selectedWebhookAliases, object? workflowData)
        {
            Name = name;
            Description = description;
            InputSchema = inputSchema;
            OutputSchema = outputSchema;
            Version = version;
            LastUpdate = lastUpdate;
            CreatedIn = createdIn;
            Status = status;
            BaseId = baseId;
            RuleId = ruleId;
            Type = type;
            Tags = tags;
            AuditLog = auditLog;
            RuleAlias = ruleAlias;
            Locked = locked;
            RuleAliasInfo = ruleAliasInfo;
            SessionId = sessionId;
            DecisionTable = decisionTable;
            VisualEditorData = visualEditorData;
            CompositionId = compositionId;
            DataTree = dataTree;
            Rules = rules;
            Nodes = nodes;
            UserVariables = userVariables;
            PreviousBaseId = previousBaseId;
            Script = script;
            SelectedWebhookAliases = selectedWebhookAliases;
            WorkflowData = workflowData;
        }

        /// <summary>
        /// Nested Builder class (equivalent to Java's static nested class)
        /// </summary>
        public class Builder
        {
            private string? _name;
            private string? _description;
            private object? _inputSchema;
            private object? _outputSchema;
            private int _version;
            private string? _status;
            private string? _baseId;
            private string? _ruleId;
            private string? _type;
            private List<string>? _tags;
            private Dictionary<string, object>? _auditLog;
            private string? _ruleAlias;
            private bool? _locked;
            private string? _ruleAliasInfo;
            private string? _sessionId;
            private Dictionary<string, object>? _decisionTable;
            private object? _visualEditorData;
            private string? _compositionId;
            private object? _dataTree;
            private List<object>? _rules;
            private List<object>? _nodes;
            private List<object>? _userVariables;
            private string? _previousBaseId;
            private object? _script;
            private List<string>? _selectedWebhookAliases;
            private object? _workflowData;

            public Builder SetName(string name)
            {
                _name = name;
                return this;
            }

            public Builder SetDescription(string description)
            {
                _description = description;
                return this;
            }

            public Builder SetInputSchema(object inputSchema)
            {
                _inputSchema = inputSchema;
                return this;
            }

            public Builder SetOutputSchema(object outputSchema)
            {
                _outputSchema = outputSchema;
                return this;
            }

            public Builder SetVersion(int version)
            {
                _version = version;
                return this;
            }

            public Builder SetStatus(string status)
            {
                _status = status;
                return this;
            }

            public Builder SetType(string type)
            {
                _type = type;
                return this;
            }

            public Builder SetTags(List<string> tags)
            {
                _tags = tags;
                return this;
            }

            public Builder SetAuditLog(Dictionary<string, object> auditLog)
            {
                _auditLog = auditLog;
                return this;
            }

            public Builder SetVisualEditorData(object visualEditorData)
            {
                _visualEditorData = visualEditorData;
                return this;
            }

            public Builder SetSelectedWebhookAliases(List<string> selectedWebhookAliases)
            {
                _selectedWebhookAliases = selectedWebhookAliases;
                return this;
            }

            public Builder SetWorkflowData(object workflowData)
            {
                _workflowData = workflowData;
                return this;

            }

            public Rule Build()
            {
                // Matches the Java builder, which passes null for un-set reference types
                // and DateTime.UtcNow for the two new Date() calls.
                return new Rule(
                    _name, _description, _inputSchema, _outputSchema, _version,
                    DateTime.UtcNow, DateTime.UtcNow, _status, _baseId, _ruleId,
                    _type, _tags, _auditLog, _ruleAlias, _locked, _ruleAliasInfo,
                    _sessionId, _decisionTable, _visualEditorData, _compositionId,
                    _dataTree, _rules, _nodes, _userVariables, _previousBaseId,
                    _script, _selectedWebhookAliases, _workflowData
                );
            }
        }
    }
}