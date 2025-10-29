using DecisionRules.Models; // Assuming namespace for Job model
using DecisionRules.Utilities;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DecisionRules.Api
{
    public class JobApi
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public JobApi(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Configure JSON options for camelCase, typical for web APIs
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Builds the specific API URL for the job endpoint.
        /// </summary>
        private Uri GetCategoryUrl(string host, string[] apiPath)
        {
            try
            {
                string baseUrl = Utils.GetBaseURL(host);
                // Filter out null or empty strings before joining
                var validPathSegments = apiPath.Where(pathParam => !string.IsNullOrEmpty(pathParam));
                string path = $"/job/{string.Join("/", validPathSegments)}";
                return new Uri(baseUrl + path);
            }
            catch (Exception e)
            {
                // Wrap the original exception for better context
                throw new Exception("Failed to create category URL", e);
            }
        }

        /// <summary>
        /// Starts a new job for a specific rule.
        /// </summary>
        public async Task<Job> StartJobApiAsync(DecisionRulesOptions options, string ruleIdOrAlias, object inputData, int? version)
        {
            try
            {
                // Handle nullable version: "" if null, otherwise the number
                string versionStr = version?.ToString() ?? "";
                Uri url = GetCategoryUrl(options.Host, new[] { "start", ruleIdOrAlias, versionStr });

                string responseJson = await Utils.DoCallAsync(_httpClient, url, options.SolverKey, HttpMethod.Post, inputData);

                return JsonSerializer.Deserialize<Job>(responseJson, _jsonOptions);
            }
            catch (Exception e)
            {
                // Propagate the exception
                throw;
            }
        }

        /// <summary>
        /// Cancels a running job.
        /// </summary>
        public async Task<Job> CancelJobApiAsync(DecisionRulesOptions options, string jobId)
        {
            try
            {
                Uri url = GetCategoryUrl(options.Host, new[] { "cancel", jobId });

                // No input data for cancel, so pass null
                string responseJson = await Utils.DoCallAsync(_httpClient, url, options.SolverKey, HttpMethod.Post);

                return JsonSerializer.Deserialize<Job>(responseJson, _jsonOptions);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the status and information for a specific job.
        /// </summary>
        public async Task<Job> GetJobInfoApiAsync(DecisionRulesOptions options, string jobId)
        {
            try
            {
                Uri url = GetCategoryUrl(options.Host, new[] { jobId });

                string responseJson = await Utils.DoCallAsync(_httpClient, url, options.SolverKey, HttpMethod.Get);

                return JsonSerializer.Deserialize<Job>(responseJson, _jsonOptions);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}