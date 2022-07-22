using DecisionRules.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class ApiBase
    {
        protected readonly string _apiKey;
        protected HttpClient _client;
        protected CustomDomain _url;
        private ApiDataUtils _apiDataUtils;
        protected JsonSerializerSettings _settings;

        public ApiBase(string apikey, CustomDomain customDomain, NamingStrategy namingStrategy)
        {
            _apiKey = apikey;
            _client = new HttpClient();
            _url = customDomain;
            _settings = NamingStrategyHandler.CreateNamingStrategy(namingStrategy);
            HttpClientUtils clientUtils = new HttpClientUtils(_client);
            _apiDataUtils = new ApiDataUtils();
            clientUtils.SetBaseHeader(_apiKey);
        }

        protected async Task<List<U>> CallSolver<T, U>(string url, T data, Enums.RuleFlowStrategy strategy)
        {
            return await CallSolver<T, U>(url, data, strategy, null);
        }

        protected async Task<List<U>> CallSolver<T,U>(string url, T data, Enums.RuleStrategy strategy)
        {
            return await CallSolver<T, U>(url, data, strategy, null);
        }

        protected async Task<List<U>> CallSolver<T, U>(string url, T data, Enums.RuleFlowStrategy strategy, string correlationId)
        {
            try
            {
                string requestData = JsonConvert.SerializeObject(_apiDataUtils.PrepareRequest<T>(data), _settings);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                if (strategy != Enums.RuleFlowStrategy.STANDARD)
                {
                    request.Headers.Add("X-Strategy", strategy.ToString());
                }

                if (correlationId != null)
                {
                    request.Headers.Add("X-Correlation-Id", correlationId);
                }

                request.Content = new StringContent(requestData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.SendAsync(request);

                return await ResponseDeserializer.DeserializeSolverResponse<U>(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task<List<U>> CallSolver<T,U>(string url, T data, Enums.RuleStrategy strategy, string correlationId)
        {
            try
            {
                string requestData = JsonConvert.SerializeObject(_apiDataUtils.PrepareRequest<T>(data), _settings);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                
                if (strategy != Enums.RuleStrategy.STANDARD)
                {
                    request.Headers.Add("X-Strategy", strategy.ToString());
                }

                if (correlationId != null)
                {
                    request.Headers.Add("X-Correlation-Id", correlationId);
                }

                request.Content = new StringContent(requestData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.SendAsync(request);

                return await ResponseDeserializer.DeserializeSolverResponse<U>(response);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
