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

namespace DecisionRules
{
    public class DecisionRulesService
    {

        private readonly HttpClient client = new HttpClient();
        private readonly RequestOption globalOptions;

        public DecisionRulesService(RequestOption options)
        {
            globalOptions = options;
        }

        public async virtual Task<List<U>> Solve<T, U>(String ruleId, T inputData, SolverStrategies solverStrategy, String version= default)
        {
            string url = UrlGenerator(ruleId, version);

            try
            {
                var response = await ApiCall<T>(url, inputData, solverStrategy);

                var result = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<U>>(result);
            }
            catch (NoUserException e)
            {
                throw e;
            }
            catch (TooManyApiCallsException e)
            {
                throw e;
            }
            catch (NotPublishedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async virtual Task<List<U>> Solve<U>(String ruleId, string inputData, SolverStrategies solverStrategy, String version = default)
        {
            string url = UrlGenerator(ruleId, version);

            try
            {
                var response = await ApiCall<string>(url, inputData, solverStrategy);

                return JsonSerializer.Deserialize<List<U>>(response.Content.ReadAsStringAsync().Result);
            }
            catch (NoUserException e)
            {
                throw e;
            }
            catch (TooManyApiCallsException e)
            {
                throw e;
            }
            catch (NotPublishedException e)
            {
                throw e;
            }
            catch (ServerErrorException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<HttpResponseMessage> ApiCall<T>(string url, T inputData, SolverStrategies solverStrategy)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", globalOptions.ApiKey);

            if(solverStrategy != SolverStrategies.STANDARD)
            {
                client.DefaultRequestHeaders.Add("X-Strategy", solverStrategy.ToString());
            }

            HttpResponseMessage response;

            if (typeof(T) != typeof(string))
            {
                var requestData = new RequestModel<T>(inputData);
                
                _ = new JsonSerializerOptions { WriteIndented = false };

                var request = JsonSerializer.Serialize(requestData);

                response = await client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));
            } else
            {
                response = client.PostAsync(url, new StringContent(inputData.ToString(), Encoding.UTF8, "application/json")).Result;
            }

            ValidateResponse(response);

            return response;
        }

        private String UrlGenerator(String ruleId, String version)
        {
            String url;

            try
            {
                if (this.globalOptions.Geoloc != default)
                {
                    url = $"http://{this.globalOptions.Geoloc}.api.decisionrules.io/rule/solve/";
                }
                else
                {
                    url = "http://api.decisionrules.io/rule/solve/";
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
            catch (Exception e)
            {
                throw e;
            }
            
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
