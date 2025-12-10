
namespace DecisionRules
{
    using DecisionRules.Api;
    using DecisionRules.Models;
    using System;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// A service client for interacting with the DecisionRules API.
    /// </summary>
    public partial class DecisionRulesService
    {
        public Management Management { get; }
        public JobService Job { get; }

        private readonly HttpClient _httpClient;
        public readonly DecisionRulesOptions _options;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly SolveApi solveApi;

        public DecisionRulesService(DecisionRulesOptions options)
        {
            _httpClient = new HttpClient();
            _options = options;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            solveApi = new SolveApi(_httpClient, _options);

            Management = new Management(this, _httpClient);
            Job = new JobService(this, _httpClient);
        }

        /// <summary>
        /// Solves a specific version of a rule with the provided data and optional solver options.
        /// </summary>
        /// <param name="ruleIdOrAlias">The ID or alias of the rule.</param>
        /// <param name="input">The input data object or JSON string.</param>
        /// <param name="version">Version of the rule (can be null).</param>
        /// <param name="solverOptions">Optional solver options.</param>
        /// <returns>The raw JSON string response from the API.</returns>
        public async Task<string> SolveAsync(string ruleIdOrAlias, object input, int? version = null, SolverOptions? solverOptions = null)
        {
            return await solveApi.SolveApiAsync(ruleIdOrAlias, input, version, solverOptions);
        }

        /// <summary>
        /// Solves a specific version of a rule with the provided data and optional solver options.
        /// </summary>
        /// <param name="ruleIdOrAlias">The ID or alias of the rule.</param>
        /// <param name="input">The input data object or JSON string.</param>
        /// <param name="version">Version of the rule (can be null).</param>
        /// <param name="solverOptions">Optional solver options.</param>
        /// <returns>The raw JSON string response from the API.</returns>
        public async Task<string> SolveAsync(string ruleIdOrAlias, string input, int? version = null, SolverOptions? solverOptions = null)
        {
            return await solveApi.SolveApiAsync(ruleIdOrAlias, input, version, solverOptions);
        }

        /// <summary>
        /// Validates a webhook signature using a timing-safe comparison.
        /// </summary>
        /// <param name="payload">The raw request body payload.</param>
        /// <param name="signature">The signature from the request header.</param>
        /// <param name="secret">Your webhook secret.</param>
        /// <returns>True if the signature is valid, otherwise false.</returns>
        public static bool ValidateWebhookSignature(string payload, string signature, string secret)
        {
            try
            {
                var secretBytes = Encoding.UTF8.GetBytes(secret);
                using var hmac = new HMACSHA256(secretBytes);
                var payloadBytes = Encoding.UTF8.GetBytes(payload);
                var hash = hmac.ComputeHash(payloadBytes);

                string expectedSignature = BytesToHex(hash);

                var signatureBytes = Encoding.UTF8.GetBytes(signature);
                var expectedSignatureBytes = Encoding.UTF8.GetBytes(expectedSignature);

                // Timing-safe comparison to prevent timing attacks
                return CryptographicOperations.FixedTimeEquals(signatureBytes, expectedSignatureBytes);
            }
            catch (Exception)
            {
                return false; // Return false on any error for security
            }
        }

        private static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
