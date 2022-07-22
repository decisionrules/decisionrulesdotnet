using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DecisionRules.Utils
{
    internal class HttpClientUtils
    {
        private HttpClient _httpClient;

        public HttpClientUtils(HttpClient client)
        {
            _httpClient = client;
        }

        public void SetBaseHeader(string apiKey)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
