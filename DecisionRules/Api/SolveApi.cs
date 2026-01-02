using DecisionRules.Models; // Assuming namespace for SolverOptions
using DecisionRules.Utilities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DecisionRules.Api
{
    public class SolveApi
    {
        private readonly HttpClient _httpClient;
        private readonly DecisionRulesOptions _options;
        private readonly JsonSerializerOptions _jsonOptions;

        public SolveApi(HttpClient httpClient, DecisionRulesOptions options)
        {
            _httpClient = httpClient;
            _options = options;

            // Configure JSON options for camelCase
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Creates the solver URL for a specific rule.
        /// </summary>
        private Uri CreateUrl(string ruleId, int? version)
        {
            // Use string interpolation
            string path = $"/rule/solve/{ruleId}";

            // C# equivalent of (version != null && version > 0)
            if (version.HasValue && version.Value > 0)
            {
                path += "/";
                path += version.Value.ToString();
            }

            try
            {
                // Assumes a static Utils.GetBaseURL method exists
                string baseUrl = Utils.GetBaseURL(_options.Host);
                return new Uri(baseUrl + path);
            }
            catch (Exception e)
            {
                // Wrap and re-throw for better context
                throw new Exception("Failed to create solver URL", e);
            }
        }

        public async Task<List<U>> SolveApiAsync<T, U>(string ruleId, T data, int? version, SolverOptions? solverOptions = null)
        {
            var response = await CallSolveApiAsync(ruleId, data, version, solverOptions);
            return await ResponseDeserializer.DeserializeSolverResponse<U>(response);
        }

        public async Task<string> SolveApiAsync(string ruleId, object data, int? version, SolverOptions? solverOptions = null)
        {
            var response = await CallSolveApiAsync(ruleId, data, version, solverOptions);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Solves a rule with the provided data.
        /// </summary>
        /// <param name="ruleId">The ID or alias of the rule to solve.</param>
        /// <param name="data">The input data. Can be an object or a pre-serialized JSON string.</param>
        /// <param name="version">The specific version of the rule to solve (optional).</param>
        /// <param name="solverOptions">
        /// Note: This parameter is included to match the Java signature, 
        /// but it was unused in the original Java logic and has no effect.
        /// </param>
        /// <returns>The raw JSON string result from the API, or null if an error occurs.</returns>
        public async Task<HttpResponseMessage> CallSolveApiAsync(string ruleId, object data, int? version, SolverOptions? solverOptions = null)
        {
            // Create the URL
            Uri url = CreateUrl(ruleId, version);
              
            // --- Logic from createHeaders ---
            if (string.IsNullOrEmpty(_options.SolverKey))
            {
                throw new ArgumentException("Solver key is not set.");
            }

            // --- Prepare Payload ---
            // Replicates: data instanceof String ? data : mapper.writeValueAsString(data)
            string dataJson = data is string dataString
                ? dataString
                : JsonSerializer.Serialize(data, _jsonOptions);

            // Replicates: String.format("{ \"data\": %s}", dataJson)
            string jsonPayload = $"{{ \"data\": {dataJson} }}";

            // --- Build and Send Request ---
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                // Set Authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.SolverKey);

                // Set Headers
                Utils.PopulateDefaultHeaders(_httpClient, _options, solverOptions);

                // Set Content and Content-Type
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Set Accept header (implied by createHeaders's ContentType setting)
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return await _httpClient.SendAsync(request);
            }
        }
    }
}