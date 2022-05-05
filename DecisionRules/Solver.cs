using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace DecisionRules
{
    public class Solver: ApiBase
    {

        public Solver(string apiKey, CustomDomain customDomain = null) : base(apiKey, customDomain){}

        public async Task<U> SolveRule<T, U>(string ruleId, T data, int version = 1, Enums.RuleStrategy strategy = Enums.RuleStrategy.STANDARD)
        {
            string url = _url.createSolverUrl(Enums.SolverMode.RULE);

            if (version == 1 && version > 0)
            {
                url += $"/{ruleId}";
            } else
            {
                url += $"/{ruleId}/{version}";
            }

            try
            {
                SetStrategyHeader(strategy);

                string request = JsonConvert.SerializeObject(PrepareRequest<T>(data), _settings);

                var response = await _client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));

                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
            } catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<U> SolveRuleFlow<T, U>(string ruleId, T data, int version = 1, Enums.RuleFlowStrategy strategy = Enums.RuleFlowStrategy.STANDARD)
        {
            string url = _url.createSolverUrl(Enums.SolverMode.RULEFLOW);

            try
            {
                SetStrategyHeader(strategy);

                string request = JsonConvert.SerializeObject(PrepareRequest<T>(data), _settings);

                HttpResponseMessage response = await _client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));

                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
            } catch(Exception e)
            {
                throw e;
            }
        }
    }

    
}
