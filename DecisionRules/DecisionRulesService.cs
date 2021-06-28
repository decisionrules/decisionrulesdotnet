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

            var response = await ApiCall<T>(url, inputData, solverStrategy);

            var result = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<List<U>>(result);
        }

        public async virtual Task<List<U>> Solve<U>(String ruleId, string inputData, SolverStrategies solverStrategy, String version = default)
        {
            string url = UrlGenerator(ruleId, version);

            var response = await ApiCall<string>(url, inputData, solverStrategy);

            return JsonSerializer.Deserialize<List<U>>(response.Content.ReadAsStringAsync().Result);
            
        }

        private async Task<HttpResponseMessage> ApiCall<T>(string url, T inputData, SolverStrategies solverStrategy)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", globalOptions.ApiKey);



            if(solverStrategy != SolverStrategies.STANDARD)
            {
                client.DefaultRequestHeaders.Add("X-Strategy", solverStrategy.ToString());
            }

            try
            {
                HttpResponseMessage response;

                if (typeof(T) != typeof(string))
                {
                    var requestData = new RequestModel<T>(inputData);

                    _ = new JsonSerializerOptions { WriteIndented = false };

                    var request = JsonSerializer.Serialize(requestData);

                    response = await client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));
                }
                else
                {
                    response = client.PostAsync(url, new StringContent(inputData.ToString(), Encoding.UTF8, "application/json")).Result;
                }

                ValidateResponse(response);

                return response;
            }
            catch (NoUserException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (TooManyApiCallsException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (NotPublishedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ServerErrorException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        private String UrlGenerator(String ruleId, String version)
        {
            String url;

            
            if (this.globalOptions.Geoloc != GeoLocations.DEFAULT)
            {
                url = $"http://{this.globalOptions.Geoloc.ToString().ToLower()}.api.decisionrules.io/rule/solve/";
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
