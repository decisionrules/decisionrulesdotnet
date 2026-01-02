using DecisionRules.Exceptions; // Required for DecisionRulesErrorException
using DecisionRules.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DecisionRules.Utilities
{
    public static class Utils
    {
        // Static serializer options to configure camelCase, replacing the static ObjectMapper
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// Gets the base URL based on the host alias.
        /// This replaces the Java enum lookup.
        /// </summary>
        public static string GetBaseURL(string host)
        {
            if (string.IsNullOrEmpty(host))
            {
                return "https://api.decisionrules.io"; // Default
            }

            // Case-insensitive comparison, matching the hostEnum.value.equalsIgnoreCase(host)
            switch (host.ToLowerInvariant())
            {
                case "global-cloud":
                case "global_cloud": // Added for robustness
                    return "https://api.decisionrules.io";

                case "region-eu":
                case "region_eu":
                    return "https://eu.api.decisionrules.io";

                case "region-us":
                case "region_us":
                    return "https://us.api.decisionrules.io";

                case "region-au":
                case "region_au":
                    return "https://au.api.decisionrules.io";

                default:
                    // If it's not a known alias, return the host as-is.
                    // This matches the Java logic of falling out of the loop.
                    return host;
            }
        }

        // The createConverter() method is omitted as it is specific to the
        // Spring Framework (MappingJackson2HttpMessageConverter).
        // In .NET, JSON serialization is configured via JsonSerializerOptions (see above)
        // or directly on the HttpClient factory.

        /// <summary>
        /// Creates a DecisionRulesErrorException from a caught exception.
        /// </summary>
        public static DecisionRulesErrorException HandleError(Exception e)
        {
            if (e != null && !string.IsNullOrEmpty(e.Message))
            {
                return new DecisionRulesErrorException(e.Message, e);
            }

            // Fallback message from the Java code
            string message = $"Call ended with status: {e?.Message ?? "Unknown"}";
            return new DecisionRulesErrorException(message, e);
        }

        // Overload for requests without a body (like GET)
        public static async Task<string> DoCallAsync(HttpClient client, Uri url, string solverKey, HttpMethod method)
        {
            return await DoCallAsync(client, url, solverKey, method, null);
        }

        /// <summary>
        /// Performs the HTTP call.
        /// This async method replaces both Java doCall methods and the createHeaders method.
        /// </summary>
        public static async Task<string> DoCallAsync(HttpClient client, Uri url, string solverKey, HttpMethod method, object body)
        {
            // HttpRequestMessage is disposable
            using (var request = new HttpRequestMessage(method, url))
            {
                // 1. Create Headers (replaces createHeaders)
                if (string.IsNullOrEmpty(solverKey))
                {
                    throw new ArgumentException("Solver key is not set.");
                }
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", solverKey);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // 2. Add Content (if body is provided)
                if (body != null)
                {
                    string jsonPayload = JsonSerializer.Serialize(body, _jsonOptions);
                    // StringContent sets the "Content-Type: application/json" header
                    request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                }

                // 3. Make the call
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // EnsureSuccessStatusCode throws an HttpRequestException if the
                    // status code is not 2xx, mimicking RestTemplate's default behavior.
                    response.EnsureSuccessStatusCode();

                    return responseBody;
                }
            }
        }

        public static void PopulateDefaultHeaders(
            HttpClient httpClient,
            DecisionRulesOptions options,
            SolverOptions? solverOptions = null)
        {
            if (!string.IsNullOrEmpty(options?.SolverKey))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", options.SolverKey);
            }
            else
            {
                throw new InvalidOperationException("Solver key missing.");
            }

            httpClient.DefaultRequestHeaders.Add("X-Debug",
                (solverOptions?.Debug ?? false).ToString().ToLower());

            if (!string.IsNullOrEmpty(solverOptions?.CorrId))
            {
                httpClient.DefaultRequestHeaders.Add("X-Correlation-Id", solverOptions.CorrId);
            }

            if (solverOptions?.Strategy != null)
            {
                httpClient.DefaultRequestHeaders.Add("X-Strategy", solverOptions?.Strategy.ToString());
            }
            else
            {
                httpClient.DefaultRequestHeaders.Add("X-Strategy", "STANDARD");
            }

            httpClient.DefaultRequestHeaders.Add("X-Audit",
                (solverOptions?.Audit ?? false).ToString().ToLower());

            if (solverOptions?.AuditTtl.HasValue == true)
            {
                httpClient.DefaultRequestHeaders.Add("X-Audit-Ttl",
                    solverOptions.AuditTtl.Value.ToString());
            }

            if (!string.IsNullOrEmpty(solverOptions?.LookupMethod.ToString()))
            {
                httpClient.DefaultRequestHeaders.Add("X-Lookup-Method",
                    solverOptions?.LookupMethod.ToString());
            }
        }
    }
}
