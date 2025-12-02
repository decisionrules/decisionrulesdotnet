using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DecisionRules.Enums
{
    /// <summary>
    /// Specifies the DecisionRules API host region. The EnumMember attribute ensures correct JSON serialization.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HostEnum
    {
        [EnumMember(Value = "global_cloud")]
        GlobalCloud,

        [EnumMember(Value = "region_eu")]
        RegionEU,

        [EnumMember(Value = "region_us")]
        RegionUS,

        [EnumMember(Value = "region_au")]
        RegionAU
    }

    /// <summary>
    /// Defines the type of a node in the folder structure.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FolderType
    {
        ROOT,
        FOLDER,
        RULE
    }

    /// <summary>
    /// Represents the status of a rule.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RuleStatus
    {
        [EnumMember(Value = "pending")]
        PENDING,

        [EnumMember(Value = "published")]
        PUBLISHED
    }

    /// <summary>
    /// Represents the category for management API operations.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MngCategoryEnum
    {
        [EnumMember(Value = "rule")]
        Rule,

        [EnumMember(Value = "space")]
        Space,

        [EnumMember(Value = "tags")]
        Tags,

        [EnumMember(Value = "folder")]
        Folder,

        [EnumMember(Value = "tools")]
        Tools,

        [EnumMember(Value = "rule-flow")]
        RuleFlow
    }

    /// <summary>
    /// Represents the category for management API operations.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StrategyOptions
    {
        [EnumMember(Value = "STANDARD")]
        STANDARD,

        [EnumMember(Value = "ARRAY")]
        ARRAY,

        [EnumMember(Value = "FIRST_MATCH")]
        FIRST_MATCH,

        [EnumMember(Value = "EVALUATE_ALL")]
        EVALUATE_ALL
    }


    /// <summary>
    /// Represents options of Lookup Table methods.
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LookupMethodOptions
    {
        [EnumMember(Value = "LOOKUP_VALUE")]
        LOOKUP_VALUE,

        [EnumMember(Value = "LOOKUP_EXISTS")]
        LOOKUP_EXISTS,
    }
}