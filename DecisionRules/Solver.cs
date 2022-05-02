using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace DecisionRules
{
    public class Solver: ApiBase
    {
        public Solver(string apiKey, CustomDomain customDomain = null): base(apiKey, customDomain){}

        public async Task<HttpContent> SolveRule<T>(string ruleId, T data, int version = 1, Enums.RuleStrategy strategy = Enums.RuleStrategy.STANDARD)
        {
            string url = _url.createSolverUrl(Enums.SolverMode.RULE);

            try
            {
                string request = JsonConvert.SerializeObject(data);

                HttpResponseMessage response = await _client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));

                return response.Content;
            } catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<HttpContent> SolveRuleFlow<T>(string ruleId, T data, int version = 1, Enums.RuleFlowStrategy strategy = Enums.RuleFlowStrategy.STANDARD)
        {
            string url = _url.createSolverUrl(Enums.SolverMode.RULEFLOW);

            try
            {
                string request = JsonConvert.SerializeObject(data);

                HttpResponseMessage response = await _client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));

                return response.Content;
            } catch(Exception e)
            {
                throw e;
            }
        }
    }
}
