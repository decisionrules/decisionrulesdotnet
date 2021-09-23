using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using DecisionRules.Exceptions;
using System.Net;
using System.Collections.Generic;
using static DecisionRules.Model.SolverStragiesEnum;
using static DecisionRules.Model.GeoLocationsEnum;
using static DecisionRules.Model.SolverTypesEnum;
using static DecisionRules.Model.ProtocolEnum;

namespace DecisionRules
{
    public class DecisionRulesService : DecisionRulesBase
    {

        public DecisionRulesService(RequestOption options) : base(options){}

        public async virtual Task<List<U>> Solve<T, U>(String ruleId, T inputData, SolverStrategies solverStrategy, String version= default, SolverTypeEnum solverType = SolverTypeEnum.RULE)
        {
            string url = UrlGenerator(ruleId, version, solverType);

            var response = await base.ApiCall<T>(url, inputData, solverStrategy);

            var result = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<List<U>>(result);
        }

        public async virtual Task<List<U>> Solve<U>(String ruleId, string inputData, SolverStrategies solverStrategy, String version = default, SolverTypeEnum solverType = SolverTypeEnum.RULE)
        {
            string url = UrlGenerator(ruleId, version, solverType);

            var response = await base.ApiCall<string>(url, inputData, solverStrategy);

            return JsonSerializer.Deserialize<List<U>>(response.Content.ReadAsStringAsync().Result);
            
        }

        

        private String UrlGenerator(String ruleId, String version, SolverTypeEnum solverType)
        {
            String url;

            if(Enum.IsDefined(typeof(SolverTypeEnum), solverType))
            {
                url = CreateUrlSolverType(ruleId, version, solverType);
            } else
            {
                throw new InvalidSolverTypeException();
            }

            return url;

        }

        private string CreateUrlSolverType(String ruleId, String version, SolverTypeEnum solverType)
        {
            String url;

            if(base.globalOptions.CustomDomain != null)
            {
                String protocol = base.globalOptions.CustomDomain.CustomDomainProtocol.ToString().ToLower();
                String apiUrl = base.globalOptions.CustomDomain.CustomDomainUrl.ToString().ToLower();

                url = $"{protocol}://{apiUrl}/{solverType.ToString().ToLower()}/solve/";
            } else
            {
                if (base.globalOptions.Geoloc != GeoLocations.DEFAULT)
                {
                    url = $"https://{base.globalOptions.Geoloc.ToString().ToLower()}.api.decisionrules.io/{solverType.ToString().ToLower()}/solve/";
                }
                else
                {
                    url = $"https://api.decisionrules.io/{solverType.ToString().ToLower()}/solve/";
                }
            }

            if (version != default)
            {
                url += $"{ruleId}/{version}";

            }
            else
            {
                url += $"{ruleId}";

            }

            return url;
        }
        private void ValidateResponse (HttpResponseMessage response)
        {
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                throw new NotPublishedException();
            }
            else if (response.StatusCode.Equals(HttpStatusCode.UpgradeRequired))
            {
                throw new TooManyApiCallsException();
            }
            else if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                throw new NoUserException();
            }
            else if (response.StatusCode.Equals(HttpStatusCode.InternalServerError) || response.StatusCode.Equals(HttpStatusCode.ServiceUnavailable))
            {
                throw new ServerErrorException();
            }
        }
    }
}
