﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class ApiBase
    {
        protected readonly string _apiKey;
        protected HttpClient _client;
        protected CustomDomain _url;
        protected JsonSerializerSettings _settings;

        public ApiBase(string apikey, CustomDomain customDomain, NamingStrategy namingStrategy)
        {
            _apiKey = apikey;
            _client = new HttpClient();
            _url = customDomain;
            _settings = CreateNamingStrategy(namingStrategy);
            SetBaseHeader();
        }

        private void SetBaseHeader()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiKey);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected ApiDataWrapper<T> PrepareRequest<T>(T userData)
        {
            ApiDataWrapper<T> request = new ApiDataWrapper<T>
            {
                Data = userData
            };

            return request;
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
                string requestData = JsonConvert.SerializeObject(PrepareRequest<T>(data), _settings);

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

                return JsonConvert.DeserializeObject<List<U>>(await response.Content.ReadAsStringAsync());
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
                string requestData = JsonConvert.SerializeObject(PrepareRequest<T>(data), _settings);

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

                return JsonConvert.DeserializeObject<List<U>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private JsonSerializerSettings CreateNamingStrategy(NamingStrategy namingStrategy)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            };
        }

    }

    public class ApiDataWrapper<T>
    {
        public T Data { get; set; }
    }
}
