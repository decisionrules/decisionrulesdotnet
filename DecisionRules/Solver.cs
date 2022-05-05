using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class Solver: ApiBase
    {

        public Solver(string apiKey, CustomDomain customDomain = null) : base(apiKey, customDomain){}

        public async Task<U> SolveRule<T, U>(string ruleId, T data, int version = 1, Enums.RuleStrategy strategy = Enums.RuleStrategy.STANDARD)
        {
            string url = _url.createSolverUrl(Enums.SolverMode.RULE);

            url += SetRuleIdAndVersion(ruleId, version);

            try
            {
                SetStrategyHeader(strategy);

                string request = JsonConvert.SerializeObject(PrepareRequest<T>(data), _settings);

                Console.WriteLine(request);

                HttpResponseMessage response = await _client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));

                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
            } catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<U> SolveRuleFlow<T, U>(string ruleId, T data, int version = 1, Enums.RuleFlowStrategy strategy = Enums.RuleFlowStrategy.STANDARD)
        {
            string url = _url.createSolverUrl(Enums.SolverMode.RULEFLOW);

            url += SetRuleIdAndVersion(ruleId, version);

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
