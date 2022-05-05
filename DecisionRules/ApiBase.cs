using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DecisionRules
{
    public class ApiBase
    {
        protected readonly string _apiKey;
        protected readonly CustomDomain _customDomain;
        protected HttpClient _client;
        protected Url _url;
        protected DefaultContractResolver _contractResolver;
        protected JsonSerializerSettings _settings;


        public ApiBase(string apiKey, CustomDomain customDomain)
        {
            _apiKey = apiKey;
            _customDomain = customDomain;
            _client = new HttpClient();
            _url = new Url(_customDomain);
            _contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            _settings = new JsonSerializerSettings
            {
                ContractResolver = _contractResolver,
                Formatting = Formatting.Indented,
            };
            SetBaseHeader();
        }

        private void SetBaseHeader()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiKey);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void SetStrategyHeader(Enums.RuleStrategy strategy)
        {
            if (strategy != Enums.RuleStrategy.STANDARD)
            {
                _client.DefaultRequestHeaders.Add("X-Strategy", strategy.ToString());
            }
        }

        protected void SetStrategyHeader(Enums.RuleFlowStrategy strategy)
        {
            if (strategy != Enums.RuleFlowStrategy.STANDARD)
            {
                _client.DefaultRequestHeaders.Add("X-Strategy", strategy.ToString());
            }
        }

        protected ApiDataWrapper<T> PrepareRequest<T>(T userData)
        {
            ApiDataWrapper<T> request = new ApiDataWrapper<T>
            {
                Data = userData
            };

            return request;
        }

    }

    public class ApiDataWrapper<T>
    {
        public T Data { get; set; }
    }
}
