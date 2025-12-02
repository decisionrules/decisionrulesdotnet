using DecisionRules.Enums;
using System.Reflection;
using System.Runtime.Serialization;

namespace DecisionRules
{
    /// <summary>
    /// Configuration options for the DecisionRulesService.
    /// </summary>
    public class DecisionRulesOptions
    {
        /// <summary>
        /// Gets or sets the host for the DecisionRules API.
        /// This can be a full URL or a predefined region string (e.g., "global_cloud").
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the API key for the Solver API.
        /// </summary>
        public string? SolverKey { get; set; }

        /// <summary>
        /// Gets or sets the API key for the Management API.
        /// </summary>
        public string? ManagementKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecisionRulesOptions"/> class with a custom host string.
        /// </summary>
        /// <param name="host">The base URL of the DecisionRules API or a predefined region string.</param>
        /// <param name="solverKey">Your solver API key.</param>
        /// <param name="managementKey">Your management API key.</param>
        public DecisionRulesOptions(string host, string? solverKey = null, string? managementKey = null)
        {
            this.Host = host;
            this.SolverKey = solverKey;
            this.ManagementKey = managementKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecisionRulesOptions"/> class with a predefined host region.
        /// </summary>
        /// <param name="host">The predefined <see cref="HostEnum"/> region.</param>
        /// <param name="solverKey">Your solver API key.</param>
        /// <param name="managementKey">Your management API key.</param>
        public DecisionRulesOptions(HostEnum host, string? solverKey = null, string? managementKey = null)
            : this(GetHostStringFromEnum(host), solverKey, managementKey)
        {
        }

        /// <summary>
        /// Helper method to get the string value from the [EnumMember] attribute of a HostEnum member.
        /// </summary>
        private static string GetHostStringFromEnum(HostEnum host)
        {
            var memberInfo = typeof(HostEnum).GetField(host.ToString());
            var attribute = memberInfo?.GetCustomAttribute<EnumMemberAttribute>();
            return attribute?.Value ?? host.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Creates a new builder for constructing DecisionRulesOptions.
        /// </summary>
        public static Builder CreateBuilder() => new Builder();

        /// <summary>
        /// Builder class for creating DecisionRulesOptions instances.
        /// </summary>
        public class Builder
        {
            private string? _host;
            private string? _solverKey;
            private string? _managementKey;

            /// <summary>
            /// Sets the host using a custom URL string.
            /// </summary>
            public Builder WithHost(string host)
            {
                _host = host;
                return this;
            }

            /// <summary>
            /// Sets the host using a predefined HostEnum region.
            /// </summary>
            public Builder WithHost(HostEnum host)
            {
                _host = GetHostStringFromEnum(host);
                return this;
            }

            /// <summary>
            /// Sets the Solver API key.
            /// </summary>
            public Builder WithSolverKey(string? solverKey)
            {
                _solverKey = solverKey;
                return this;
            }

            /// <summary>
            /// Sets the Management API key.
            /// </summary>
            public Builder WithManagementKey(string? managementKey)
            {
                _managementKey = managementKey;
                return this;
            }

            /// <summary>
            /// Builds and returns the DecisionRulesOptions instance.
            /// </summary>
            /// <exception cref="InvalidOperationException">Thrown when Host is not set.</exception>
            public DecisionRulesOptions Build()
            {
                if (string.IsNullOrEmpty(_host))
                {
                    throw new InvalidOperationException("Host must be set before building DecisionRulesOptions.");
                }

                return new DecisionRulesOptions(_host, _solverKey, _managementKey);
            }
        }
    }
}