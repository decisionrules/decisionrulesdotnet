using DecisionRules.Enums; // Assuming namespace for MngCategoryEnum, RuleStatus
using DecisionRules.Models; // Assuming namespace for Rule, FolderData, etc.
using DecisionRules.Utilities;
using Microsoft.AspNetCore.WebUtilities; // For QueryHelpers. Requires NuGet: Microsoft.AspNetCore.Web.Utilities
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DecisionRules.Api
{
    public class ManagementApi
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ManagementApi(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Configure JSON options for camelCase, typical for web APIs
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // Ignore null values when serializing (e.g., for options objects)
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        #region URL Helpers

        /// <summary>
        /// Builds the API URL with dictionary-based query parameters.
        /// </summary>
        private Uri GetCategoryUrl(string host, MngCategoryEnum category, string[] apiPath, Dictionary<string, string> queryParams = null)
        {
            try
            {
                string baseUrl = Utils.GetBaseURL(host);
                var validPathSegments = apiPath.Where(pathParam => !string.IsNullOrEmpty(pathParam));
                string path = $"/api/{category.GetValue()}/{string.Join("/", validPathSegments)}";

                Uri fullUrl = new Uri(new Uri(baseUrl), path);

                if (queryParams != null && queryParams.Count > 0)
                {
                    // Filter out null value entries, matching Java's filter
                    var validParams = queryParams
                        .Where(entry => entry.Value != null)
                        .ToDictionary(entry => entry.Key, entry => entry.Value);

                    fullUrl = new Uri(QueryHelpers.AddQueryString(fullUrl.ToString(), validParams));
                }

                return fullUrl;
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create category URL", e);
            }
        }

        /// <summary>
        /// Builds the API URL for the Tags category with string array query.
        /// </summary>
        private Uri GetCategoryUrl(string host, MngCategoryEnum category, string[] apiPath, string[] queryParams)
        {
            try
            {
                if (category != MngCategoryEnum.Tags)
                {
                    throw new Exception("This method overload can be used only for TAGS category.");
                }

                string baseUrl = Utils.GetBaseURL(host);
                var validPathSegments = apiPath.Where(pathParam => !string.IsNullOrEmpty(pathParam));
                string path = $"/api/{category.GetValue()}/{string.Join("/", validPathSegments)}";

                string fullUrl = baseUrl + path;

                if (queryParams != null && queryParams.Length > 0)
                {
                    // Replicates the "tags=a,b,c" query format
                    string tagsQuery = string.Join(",", queryParams.Select(tag => tag.Trim()));
                    fullUrl = QueryHelpers.AddQueryString(fullUrl, "tags", tagsQuery);
                }

                return new Uri(fullUrl);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to create TAGS category URL", e);
            }
        }

        #endregion

        #region API Methods

        public async Task<Models.Rule> GetRuleApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, int? version, RuleOptions ruleOptions = null)
        {
            string versionString = GetRuleVersion(version, ruleOptions);
            Dictionary<string, string> query = CreateDataMap(ruleOptions);

            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, new[] { ruleIdOrAlias, versionString }, query);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<Models.Rule>(responseJson, _jsonOptions);
        }

        public async Task<Models.Rule> UpdateRuleStatusApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, RuleStatus status, int? version)
        {
            string versionString = GetRuleVersion(version, null);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, new[] { "status", ruleIdOrAlias, status.ToString().ToLower(), versionString });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Put);
            Console.WriteLine("responseJson" + responseJson);
            return JsonSerializer.Deserialize<Models.Rule>(responseJson, _jsonOptions);
        }

        public async Task<Models.Rule> UpdateRuleApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, Models.Rule data, int? version)
        {
            string versionString = GetRuleVersion(version, null);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, new[] { ruleIdOrAlias, versionString });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Put, data);
            return JsonSerializer.Deserialize<Models.Rule>(responseJson, _jsonOptions);
        }

        public async Task<Models.Rule> CreateRuleApiAsync(DecisionRulesOptions options, object data, RuleOptions ruleOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(ruleOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, Array.Empty<string>(), query);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Post, data);
            return JsonSerializer.Deserialize<Models.Rule>(responseJson, _jsonOptions);
        }

        public async Task<Models.Rule> CreateNewRuleVersionApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, Models.Rule data)
        {
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, new[] { ruleIdOrAlias, "new-version" });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Post, data);
            return JsonSerializer.Deserialize<Models.Rule>(responseJson, _jsonOptions);
        }

        public async Task<string> DeleteRuleApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, int? version, RuleOptions ruleOptions = null)
        {
            string versionString = GetRuleVersion(version, ruleOptions); // Note: Java code had (version, null)
            Dictionary<string, string> query = CreateDataMap(ruleOptions);

            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, new[] { ruleIdOrAlias, versionString }, query);

            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Delete);
        }

        public async Task<string> LockRuleApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, bool locked, int? version, RuleOptions ruleOptions = null)
        {
            string versionString = GetRuleVersion(version, ruleOptions);
            Dictionary<string, string> query = CreateDataMap(ruleOptions);

            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Rule, new[] { "lock", ruleIdOrAlias, versionString }, query);

            // Use anonymous object for simple JSON bodies
            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Patch, new { locked });
        }

        public async Task<Duplicates> FindDuplicatesApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, int? version)
        {
            string versionString = GetRuleVersion(version, null);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Tools, new[] { "duplicates", ruleIdOrAlias, versionString });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<Duplicates>(responseJson, _jsonOptions);
        }

        public async Task<Dependencies> FindDependenciesApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, int? version)
        {
            string versionString = GetRuleVersion(version, null);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Tools, new[] { "dependencies", ruleIdOrAlias, versionString });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<Dependencies>(responseJson, _jsonOptions);
        }

        public async Task<Models.Rule[]> GetRulesForSpaceApiAsync(DecisionRulesOptions options)
        {
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Space, new[] { "items" });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<Models.Rule[]>(responseJson, _jsonOptions);
        }

        public async Task<Models.Rule[]> GetRulesByTagsApiAsync(DecisionRulesOptions options, string[] tags)
        {
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Tags, new[] { "items" }, tags);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<Models.Rule[]>(responseJson, _jsonOptions);
        }

        public async Task<string[]> AddTagsApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, string[] tags, int? version)
        {
            string versionString = GetRuleVersion(version, null);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Tags, new[] { ruleIdOrAlias, versionString });

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Patch, tags);
            return JsonSerializer.Deserialize<string[]>(responseJson, _jsonOptions);
        }

        public async Task<string> DeleteTagsApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, string[] tags, int? version)
        {
            string versionString = GetRuleVersion(version, null);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Tags, new[] { ruleIdOrAlias, versionString }, tags);

            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Delete);
        }

        public async Task<string> CreateFolderApiAsync(DecisionRulesOptions options, string targetNodeId, FolderData data, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { targetNodeId }, query);
            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Post, data);
        }

        public async Task<FolderData> UpdateNodeFolderStructureApiAsync(DecisionRulesOptions options, string targetNodeId, FolderData data, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { targetNodeId }, query);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Put, data);
            return JsonSerializer.Deserialize<FolderData>(responseJson, _jsonOptions);
        }

        public async Task<FolderExport> ExportFolderApiAsync(DecisionRulesOptions options, string targetNodeId, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { "export", targetNodeId }, query);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<FolderExport>(responseJson, _jsonOptions);
        }

        public async Task<FolderImport> ImportFolderApiAsync(DecisionRulesOptions options, string targetNodeId, object data, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { "import", targetNodeId }, query);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Post, data);
            return JsonSerializer.Deserialize<FolderImport>(responseJson, _jsonOptions);
        }

        public async Task<FolderData> GetFolderStructureApiAsync(DecisionRulesOptions options, string targetNodeId, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { targetNodeId }, query);

            string responseJson = await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Get);
            return JsonSerializer.Deserialize<FolderData>(responseJson, _jsonOptions);
        }

        public async Task<string> DeleteFolderApiAsync(DecisionRulesOptions options, string targetNodeId, bool deleteAll, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            query["deleteAll"] = deleteAll.ToString(); // Adds the query param

            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { targetNodeId }, query);

            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Delete);
        }

        public async Task<string> RenameFolderApiAsync(DecisionRulesOptions options, string targetNodeId, string name, FolderOptions folderOptions = null)
        {
            Dictionary<string, string> query = CreateDataMap(folderOptions);
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { "rename", targetNodeId }, query);

            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Patch, new { name });
        }

        public async Task<string> MoveFolderApiAsync(DecisionRulesOptions options, string targetNodeId, FolderNode[] nodes, string targetPath = null)
        {
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { "move" });

            // Use null-coalescing operator '??' to replicate Java logic
            object body = new
            {
                nodes,
                targetPath = targetPath ?? targetNodeId
            };

            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Put, body);
        }

        public async Task<string> FindFolderOrRuleByAttributeApiAsync(DecisionRulesOptions options, FindOptions findOptions)
        {
            Uri url = GetCategoryUrl(options.Host, MngCategoryEnum.Folder, new[] { "find" });

            // Pass the findOptions object directly as the body
            return await Utils.DoCallAsync(_httpClient, url, options.ManagementKey, HttpMethod.Post, findOptions);
        }


        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the version string, or an empty string if null or if ruleOptions.version is set.
        /// </summary>
        private string GetRuleVersion(int? version, RuleOptions ruleOptions)
        {
            if (ruleOptions?.Version != null)
            {
                return "";
            }
            return version?.ToString() ?? "";
        }

        /// <summary>
        /// Converts an options object (like RuleOptions or FolderOptions) into a dictionary
        /// for use as query parameters. This mimics Java's mapper.convertValue.
        /// </summary>
        private Dictionary<string, string> CreateDataMap(object data)
        {
            if (data == null)
            {
                return new Dictionary<string, string>();
            }

            // Serialize the object to JSON, then deserialize it into a dictionary
            string json = JsonSerializer.Serialize(data, _jsonOptions);
            Console.WriteLine("json" + json);
            var map = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions);

            // Convert JsonElement values to strings
            return map.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ValueKind == JsonValueKind.String
                       ? kvp.Value.GetString()
                       : kvp.Value.GetRawText()
            );
        }

        #endregion
    }

    #region Enum Extensions

    // Assumed helper class to get string values from enums
    // You must create this (and the enums) based on your Java code.
    public static class EnumExtensions
    {
        public static string GetValue(this MngCategoryEnum category)
        {
            // This logic MUST match your Java MngCategoryEnum.value
            switch (category)
            {
                case MngCategoryEnum.Rule: return "rule";
                case MngCategoryEnum.Space: return "space";
                case MngCategoryEnum.Tags: return "tags";
                case MngCategoryEnum.Folder: return "folder";
                case MngCategoryEnum.Tools: return "tools";
                default: throw new ArgumentException("Unknown category", nameof(category));
            }
        }
    }

    #endregion
}