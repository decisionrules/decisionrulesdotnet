using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules
{
    public class Management: ApiBase
    {
        private readonly string _urlBase;

        public Management(string apiKey) : this(apiKey, new CustomDomain("api.decisionrules.io", Enums.Protocol.HTTPS, 80), new CamelCaseNamingStrategy()) { }
        public Management(string apiKey, CustomDomain customDomain) : this(apiKey, customDomain, new CamelCaseNamingStrategy()) { }
        public Management(string apiKey, CustomDomain customDomain, NamingStrategy namingStrategy) : base(apiKey, customDomain, namingStrategy) 
        {
            _urlBase = base._url.CreateManagementUrl();
        }
        
        public async Task<U> GetRule<U>(string itemId)
        {
            return await GetRule<U>(itemId, 1);
        }
        public async Task<U> GetRule<U>(string itemId, int version)
        {
            string taskUrl = _urlBase;

            if (version > 1)
            {
                taskUrl += $"/rule/{itemId}/{version}";
            }
            else
            {
                taskUrl += $"/rule/{itemId}";
            }

            HttpResponseMessage respose = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await respose.Content.ReadAsStringAsync());
        }

        public async Task<U> GetSpaceItems<U>()
        {
            string taskUrl = $"{_urlBase}/space/items";

            HttpResponseMessage respose = await _client.GetAsync(taskUrl);

            Console.WriteLine(respose.Content.ReadAsStringAsync().Result);

            return JsonConvert.DeserializeObject<U>(await respose.Content.ReadAsStringAsync()); ;
        }
        public async Task<U> GetSpaceItems<U>(string[] tags)
        {
            string joinedTags = string.Join(",", tags);

            string taskUrl = $"{_urlBase}/tags/items/?tags={joinedTags}";

            HttpResponseMessage response = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> CreateRule<T, U>(T body)
        {
            string taskUrl = $"{_urlBase}/rule/";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> CreateRule<T, U>(string spaceId, T body)
        {
            string taskUrl = $"{_urlBase}/rule/{spaceId}";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpStatusCode> UpdateRule<T>(string itemId, T body)
        {
            return await UpdateRule<T>(itemId, body, 1);
        }

        public async Task<HttpStatusCode> UpdateRule<T>(string itemId, T body, int version)
        {
            string taskUrl = $"{_urlBase}/rule/{itemId}/{version}";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PutAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteRule(string itemId, int version)
        {
            string taskUrl = $"{_urlBase}/rule/{itemId}/{version}";

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }

        public async Task<U> GetRuleFlow<U>(string itemId)
        {
            return await GetRuleFlow<U>(itemId, 1);
        }

        public async Task<U> GetRuleFlow<U>(string itemId, int version)
        {
            string taskUrl = _urlBase;

            if (version > 1)
            {
                taskUrl += $"/rule-flow/{itemId}/{version}";
            }
            else
            {
                taskUrl += $"/rule-flow/{itemId}";
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

        public async Task<HttpStatusCode> UpdateRuleFlow<T>(string itemId, T body)
        {
            return await UpdateRuleFlow<T>(itemId, body, 1);
        }

        public async Task<HttpStatusCode> UpdateRuleFlow<T>(string itemId, T body, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/{itemId}/{version}";

            string request = JsonConvert.SerializeObject(body, _settings);

            HttpResponseMessage response = await _client.PutAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteRuleFlow(string itemId, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/{itemId}/{version}";

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }

        public async Task<U> ExportRuleFlow<U>(string itemId)
        {
            return await ExportRuleFlow<U>(itemId, 1);
        }

        public async Task<U> ExportRuleFlow<U>(string itemId, int version)
        {
            string taskUrl = _urlBase;

            if (version > 1)
            {
                taskUrl += $"/rule-flow/export/{itemId}/{version}";
            }
            else
            {
                taskUrl += $"/rule-flow/export/{itemId}";
            }

            HttpResponseMessage response = await _client.GetAsync(taskUrl);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> ImportRuleFlow<T, U>(T data)
        {
            return await ImportRuleFlow<T, U>(data, null, 1);
        }

        public async Task<U> ImportRuleFlow<T, U>(T data, string itemId)
        {
            return await ImportRuleFlow<T, U>(data, itemId, 1);
        }

        public async Task<U> ImportRuleFlow<T, U>(T data, string itemId, int version)
        {
            string taskUrl = "";

            if (string.IsNullOrEmpty(itemId) && version == 1)
            {
                taskUrl = $"{_urlBase}/rule-flow/import";
            } else if (!string.IsNullOrEmpty(itemId) && version == 1)
            {
                taskUrl = $"{_urlBase}/rule-flow/import/?new-version={itemId}";
            } else if (!string.IsNullOrEmpty(itemId) && version > 1)
            {
                taskUrl = $"{_urlBase}/rule-flow/import/?overwrite={itemId}&version={version}";
            }

            string request = JsonConvert.SerializeObject(data, _settings);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }


        public async Task<U> ChangeRuleStatus<U>(string itemId, string status, int version)
        {
            string taskUrl = $"{_urlBase}/rule/status/{itemId}/{status}/{version}";

            HttpResponseMessage response = await _client.PutAsync(taskUrl, null);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> ChangeRuleFlowStatus<U>(string itemId, string status, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/status/{itemId}/{status}/{version}";

            HttpResponseMessage response = await _client.PutAsync(taskUrl, null);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<U> UpdateTags<T, U>(T data, string itemId)
        {
            return await this.UpdateTags<T, U>(data, itemId, 1);
        }

        public async Task<U> UpdateTags<T, U>(T data, string itemId, int version)
        {
            string taskUrl = $"{_urlBase}/tags/{itemId}/{version}";

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), taskUrl);

            var requestData = JsonConvert.SerializeObject(data, _settings);

            request.Content = new StringContent(requestData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            return JsonConvert.DeserializeObject<U>(await response.Content.ReadAsStringAsync());
        }

        public async Task<HttpStatusCode> DeleteTags(string itemId, string[] tags, int version = 0)
        {
            string taskUrl = _urlBase;

            var xtags = string.Join(",", tags);

            if (version > 0)
            {
                taskUrl += $"/tags/{itemId}/{version}/?tags={xtags}";
            }
            else
            {
                taskUrl += $"/tags/{itemId}/?tags={xtags}";
            }

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }
    }
}
