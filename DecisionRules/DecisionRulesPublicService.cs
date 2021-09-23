using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class DecisionRulesPublicService : DecisionRulesBase
    {
        

        public DecisionRulesPublicService(RequestOption options): base(options){}

        private String domainFactory()
        {
            if (base.globalOptions.CustomDomain != null)
            {
                String protocol = base.globalOptions.CustomDomain.CustomDomainProtocol.ToString().ToLower();
                String apiUrl = base.globalOptions.CustomDomain.CustomDomainUrl.ToString().ToLower();

                return $"{protocol}://{apiUrl}/api";
            }
            else
            {
                return "https://api.decisionrules.io/api";
            }
        }

        public async Task<dynamic> GetRuleById(String ruleId)
        {
            String url = domainFactory() + $"/rule/{ruleId}";

            try
            {
                var response = await base.ApiCallGet(url);

                response.EnsureSuccessStatusCode();

                var responseString = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result;

            } catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<dynamic> GetRuleByIdAndVersion(String ruleId, String version)
        {
            String url = domainFactory() + $"/rule/{ruleId}/{version}";

            try
            {
                var response = await base.ApiCallGet(url);

                response.EnsureSuccessStatusCode();

                var responseString = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<dynamic> GetSpaceInfo(String spaceId)
        {
            String url = domainFactory() + $"/space/{spaceId}";

            try
            {
                var response = await base.ApiCallGet(url);

                response.EnsureSuccessStatusCode();

                var responseString = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<dynamic> PostRuleForSpace<T>(String spaceId, T inputData)
        {
            String url = domainFactory() + $"/rule/{spaceId}";

            try
            {
                var response = await base.ApiCall<T>(url, inputData);

                response.EnsureSuccessStatusCode();

                var responseString = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<dynamic> PutRule<T>(String ruleId, String version, T inputData)
        {
            String url = domainFactory() + $"/rule/{ruleId}/{version}";

            try
            {
                var response = await base.ApiCallPut<T>(url, inputData);

                response.EnsureSuccessStatusCode();

                var responseString = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<dynamic> DeleteRule(String ruleId, String version)
        {
            String url = domainFactory() + $"/rule/{ruleId}/{version}";

            try
            {
                var response = await base.ApiCallDelete(url);

                var responseString = response.Content.ReadAsStringAsync().Result;

                dynamic result = JsonConvert.DeserializeObject(responseString);

                return result;
            } 
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
