using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DecisionRules
{
    public class Management: ApiBase
    {
        private readonly string _urlBase;

        public Management(string apiKey, CustomDomain customDomain) : base(apiKey, customDomain) 
        {
            _urlBase = base._url.createManagementUrl();
        }
        
        public async Task<U> GetRule<U>(string ruleId, int version = 1)
        {
            string taskUrl = _urlBase;

            if (version > 1)
            {
                taskUrl += $"/rule/{ruleId}/{version}";
            } else
            {
                taskUrl += $"/rule/{ruleId}";
            }

            HttpResponseMessage respose = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await respose.Content.ReadAsStringAsync());
        }

        public async Task<U> GetSpace<U>()
        {
            string taskUrl = $"{_urlBase}/space/items";

            HttpResponseMessage respose = await _client.GetAsync(taskUrl);

            Console.WriteLine(respose.Content.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<U>(await respose.Content.ReadAsStringAsync()); ;
        }

        public async Task<U> CreateRule<T, U>(string spaceId, T body)
        {
            string taskUrl = $"{_urlBase}/rule/{spaceId}";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpStatusCode> UpdateRule<T>(string ruleId, T body, int version = 1)
        {
            string taskUrl = $"{_urlBase}/rule/{ruleId}/{version}";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PutAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteRule(string ruleId, int version)
        {
            string taskUrl = $"{_urlBase}/rule/{ruleId}/{version}";

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }

        public async Task<U> GetRuleFlow<U>(string ruleId, int version = 1)
        {
            string taskUrl = _urlBase;

            if (version > 1)
            {
                taskUrl += $"/rule-flow/{ruleId}/{version}";
            }
            else
            {
                taskUrl += $"/rule-flow/{ruleId}";
            }

            HttpResponseMessage respose = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await respose.Content.ReadAsStringAsync());
        }

        public async Task<U> CreateRuleFlow<T, U>(T body)
        {
            string taskUrl = $"{_urlBase}/rule-flow";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpStatusCode> UpdateRuleFlow<T>(string ruleId, T body, int version = 1)
        {
            string taskUrl = $"{_urlBase}/rule-flow/{ruleId}/{version}";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PutAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteRuleFlow(string ruleId, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/{ruleId}/{version}";

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }

        public async Task<U> ExportRuleFlow<U>(string ruleId, int version=1)
        {
            string taskUrl = _urlBase;

            if (version > 1)
            {
                taskUrl += $"/rule-flow/export/{ruleId}/{version}";
            }
            else
            {
                taskUrl += $"/rule-flow/export/{ruleId}";
            }

            HttpResponseMessage response = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> ImportRuleFlow<T, U>(T data, string ruleId = null, int version = 0)
        {
            string taskUrl = _urlBase;
            
            if (ruleId == null && version == 0)
            {
                taskUrl += $"/rule-flow/import";
            }

            if (ruleId != null && version == 0)
            {
                taskUrl += $"/rule-flow/import/?new-version={ruleId}";
            }

            if (ruleId != null && version > 0)
            {
                taskUrl += $"/rule-flow/import/?overwrite={ruleId}&version={version}";
            }

            Console.WriteLine(taskUrl);

            string request = JsonConvert.SerializeObject(data, _settings);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> ChangeRuleStatus<U>(string ruleId, string status, int version)
        {
            string taskUrl = $"{_urlBase}/rule/status/{ruleId}/{status}/{version}";

            HttpResponseMessage response = await _client.PutAsync(taskUrl, null);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> ChangeRuleFlowStatus<U>(string ruleId, string status, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/status/{ruleId}/{status}/{version}";

            HttpResponseMessage response = await _client.PutAsync(taskUrl, null);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> GetTags<U>(string[] tags)
        {
            string joinedTags = string.Join(",", tags);

            string taskUrl = $"{_urlBase}/tags/items/?tags={joinedTags}";

            HttpResponseMessage response = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> UpdateTags<T, U>(T data, string ruleId, int version = 0)
        {
            string taskUrl = _urlBase;

            if (version > 0)
            {
                taskUrl += $"/tags/{ruleId}/{version}";
            } else
            {
                taskUrl += $"/tags/{ruleId}";
            }

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), taskUrl);

            var requestData = JsonConvert.SerializeObject(data, _settings);

            request.Content = new StringContent(requestData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpStatusCode> DeleteTags(string ruleId, string[] tags, int version = 0)
        {
            string taskUrl = _urlBase;

            var xtags = string.Join(",", tags);

            if (version > 0)
            {
                taskUrl += $"/tags/{ruleId}/{version}/?tags={xtags}";
            }
            else
            {
                taskUrl += $"/tags/{ruleId}/?tags={xtags}";
            }

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }
    }
}
