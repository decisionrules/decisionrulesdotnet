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

        public ApiBase(string apiKey, CustomDomain customDomain)
        {
            _apiKey = apiKey;
            _customDomain = customDomain;
            _client = new HttpClient();
            _url = new Url(_customDomain);
            SetHeader();
        }

        private void SetHeader()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiKey);
        }

    }
}
