﻿using System;
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
        
        public async Task<HttpContent> getRule(string ruleId, int version = 1)
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

            return respose.Content;
        }

        public async Task<HttpContent> getSpace()
        {
            string taskUrl = $"{_urlBase}/space/items";

            HttpResponseMessage respose = await _client.GetAsync(taskUrl);

            return respose.Content;
        }

        public async Task<HttpContent> createRule<T>(string spaceId, T body)
        {
            string taskUrl = $"{_urlBase}/rule/{spaceId}";

            string request = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.Content;
        }

        public async Task<HttpStatusCode> updateRule<T>(string ruleId, T body, int version = 1)
        {
            string taskUrl = $"{_urlBase}/rule/{ruleId}/{version}";

            string request = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await _client.PutAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> deleteRule(string ruleId, int version)
        {
            string taskUrl = $"{_urlBase}/rule/{ruleId}/{version}";

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }

        public async Task<HttpContent> getRuleFlow(string ruleId, int version = 1)
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

            return respose.Content;
        }

        public async Task<HttpContent> createRuleFlow<T>(T body)
        {
            string taskUrl = $"{_urlBase}/rule-flow";

            string request = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.Content;
        }

        public async Task<HttpStatusCode> updateRuleFlow<T>(string ruleId, T body, int version = 1)
        {
            string taskUrl = $"{_urlBase}/rule-flow/{ruleId}/{version}";

            string request = JsonConvert.SerializeObject(body);

            HttpResponseMessage response = await _client.PutAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> deleteRuleFlow(string ruleId, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/{ruleId}/{version}";

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }

        public async Task<HttpContent> exportRuleFlow(string ruleId, int version=1)
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

            return response.Content;
        }

        public async Task<HttpContent> importRuleFlow<T>(T data, string ruleId = null, int version = 0)
        {
            string taskUrl = _urlBase;
            
            if (ruleId == null && version == 0)
            {
                taskUrl += $"/rule-flow/import";
            }

            if (ruleId != null)
            {
                taskUrl += $"/rule-flow/import/?new-version={ruleId}";
            }

            if (ruleId != null && version > 0)
            {
                taskUrl += $"/rule-flow/import/?overwrite={ruleId}&version={version}";
            }

            string request = JsonConvert.SerializeObject(data);

            HttpResponseMessage response = await _client.PostAsync(taskUrl, new StringContent(request, Encoding.UTF8, "application/json"));

            return response.Content;
        }

        public async Task<HttpContent> changeRuleStatus(string ruleId, string status, int version)
        {
            string taskUrl = $"{_urlBase}/rule/status/{ruleId}/{status}/{version}";

            HttpResponseMessage response = await _client.PutAsync(taskUrl, null);
            
            return response.Content;
        }

        public async Task<HttpContent> changeRuleFlowStatus(string ruleId, string status, int version)
        {
            string taskUrl = $"{_urlBase}/rule-flow/status/{ruleId}/{status}/{version}";

            HttpResponseMessage response = await _client.PutAsync(taskUrl, null);

            return response.Content;
        }

        public async Task<HttpContent> getTags(string[] tags)
        {
            string joinedTags = string.Join(",", tags);

            string taskUrl = $"{_urlBase}/tags/items/?tags={joinedTags}";

            HttpResponseMessage response = await _client.GetAsync(taskUrl);

            return response.Content;
        }

        public async Task<HttpContent> updateTags<T>(T data, string ruleId, int version = 0)
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

            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            return response.Content;
        }

        public async Task<HttpStatusCode> deleteTags(string ruleId, int version = 0)
        {
            string taskUrl = _urlBase;

            if (version > 0)
            {
                taskUrl += $"/tags/{ruleId}/{version}";
            }
            else
            {
                taskUrl += $"/tags/{ruleId}";
            }

            HttpResponseMessage response = await _client.DeleteAsync(taskUrl);

            return response.StatusCode;
        }
    }
}
