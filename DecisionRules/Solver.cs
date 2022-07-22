using DecisionRules.Utils;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class Solver: ApiBase
    {
        private readonly SolverUtils _solverUtils;

        public Solver(string apiKey) : this(apiKey, new CustomDomain("api.decisionrules.io", Enums.Protocol.HTTPS, 443), new CamelCaseNamingStrategy()) {}
        public Solver(string apiKey, CustomDomain customDomain) : this(apiKey, customDomain, new CamelCaseNamingStrategy()) { }
        public Solver(string apiKey, CustomDomain customDomain, NamingStrategy namingStrategy) : base(apiKey, customDomain, namingStrategy){
            _solverUtils = new SolverUtils();
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data)
        {
            return await SolveRule<T, U>(itemId, data, 0, Enums.RuleStrategy.STANDARD, null);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, string correlationId)
        {
            return await SolveRule<T, U>(itemId, data, 0, Enums.RuleStrategy.STANDARD, correlationId);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, int version)
        {
            return await SolveRule<T, U>(itemId, data, version, Enums.RuleStrategy.STANDARD, null);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, int version, string correlationId)
        {
            return await SolveRule<T, U>(itemId, data, version, Enums.RuleStrategy.STANDARD, correlationId);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, Enums.RuleStrategy strategy)
        {
            return await SolveRule<T, U>(itemId, data, 0, strategy, null);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, Enums.RuleStrategy strategy, string correlationId)
        {
            return await SolveRule<T, U>(itemId, data, 0, strategy, correlationId);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, int version, Enums.RuleStrategy strategy)
        {
            return await SolveRule<T, U>(itemId, data, version, strategy, null);
        }

        public async Task<List<U>> SolveRule<T, U>(string itemId, T data, int version, Enums.RuleStrategy strategy, string correlationId)
        {
            string url = _url.CreateSolverUrl(Enums.SolverMode.RULE);

            url += _solverUtils.SetRuleIdAndVersion(itemId, version);

            return await CallSolver<T, U>(url, data, strategy, correlationId);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data)
        {
            return await SolveRuleFlow<T, U>(itemId, data, 0, Enums.RuleFlowStrategy.STANDARD, null);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, string correlationId)
        {
            return await SolveRuleFlow<T, U>(itemId, data, 0, Enums.RuleFlowStrategy.STANDARD, correlationId);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, int version)
        {
            return await SolveRuleFlow<T, U>(itemId, data, version, Enums.RuleFlowStrategy.STANDARD, null);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, int version, string correlationId)
        {
            return await SolveRuleFlow<T, U>(itemId, data, version, Enums.RuleFlowStrategy.STANDARD, correlationId);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, Enums.RuleFlowStrategy strategy)
        {
            return await SolveRuleFlow<T, U>(itemId, data, 0, strategy, null);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, Enums.RuleFlowStrategy strategy, string correlationId)
        {
            return await SolveRuleFlow<T, U>(itemId, data, 0, strategy, correlationId);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, int version, Enums.RuleFlowStrategy strategy)
        {
            return await SolveRuleFlow<T, U>(itemId, data, version, strategy, null);
        }

        public async Task<List<U>> SolveRuleFlow<T, U>(string itemId, T data, int version, Enums.RuleFlowStrategy strategy, string correlationId)
        {
            string url = _url.CreateSolverUrl(Enums.SolverMode.COMPOSITION);

            url += _solverUtils.SetRuleIdAndVersion(itemId, version);

            return await CallSolver<T, U>(url, data, strategy, correlationId);
        }

        
    }

    
}
