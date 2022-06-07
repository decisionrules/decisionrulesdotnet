using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class Solver: ApiBase
    {
        public Solver(string apiKey) : this(apiKey, new CustomDomain("api.decisionrules.io", Enums.Protocol.HTTPS, 443), new CamelCaseNamingStrategy()) { }
        public Solver(string apiKey, CustomDomain customDomain) : this(apiKey, customDomain, new CamelCaseNamingStrategy()) { }
        public Solver(string apiKey, CustomDomain customDomain, NamingStrategy namingStrategy) : base(apiKey, customDomain, namingStrategy){}

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data)
        {
            return await SolveRule<T, U>(itemId, data, 1, Enums.RuleStrategy.STANDARD);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, int version)
        {
            return await SolveRule<T, U>(itemId, data, version, Enums.RuleStrategy.STANDARD);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, Enums.RuleStrategy strategy)
        {
            return await SolveRule<T, U>(itemId, data, 1, strategy);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, int version, Enums.RuleStrategy strategy)
        {
            string url = _url.CreateSolverUrl(Enums.SolverMode.RULE);

            url += SetRuleIdAndVersion(itemId, version);

            return await CallSolver<T, U>(url, data, strategy);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data)
        {
            return await SolveRuleFlow<T, U>(itemId, data, 1, Enums.RuleFlowStrategy.STANDARD);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, int version)
        {
            return await SolveRuleFlow<T, U>(itemId, data, version, Enums.RuleFlowStrategy.STANDARD);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, Enums.RuleFlowStrategy strategy)
        {
            return await SolveRuleFlow<T, U>(itemId, data, 1, strategy);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, int version, Enums.RuleFlowStrategy strategy)
        {
            string url = _url.CreateSolverUrl(Enums.SolverMode.COMPOSITION);

            url += SetRuleIdAndVersion(itemId, version);

            return await CallSolver<T, U>(url, data, strategy);
        }

        private string SetRuleIdAndVersion(string id, int version)
        {
            if (version == 1 && version > 0)
            {
                return $"/{id}";
            }
            
            return $"/{id}/{version}";
        }
    }

    
}
