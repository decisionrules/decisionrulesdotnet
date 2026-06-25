using DecisionRules.Enums;
using DecisionRules.Models;
using System.Net;
using System.Reflection;

namespace DecisionRules.Test
{
    [TestClass]
    public sealed class DecisionRulesServiceHttpClientTests
    {
        [TestMethod]
        public async Task SolveAsyncUsesInjectedHttpClient()
        {
            var handler = new RecordingHttpMessageHandler();
            using var httpClient = new HttpClient(handler);
            var service = new DecisionRulesService(
                new DecisionRulesOptions("https://example.com", "solver-key"),
                httpClient);

            string response = await service.SolveAsync("rule-id", new { input = "value" });

            Assert.AreEqual("{\"ok\":true}", response);
            Assert.AreEqual(1, handler.Requests.Count);
            Assert.AreEqual(HttpMethod.Post, handler.Requests[0].Method);
            Assert.AreEqual("https://example.com/rule/solve/rule-id", handler.Requests[0].RequestUri?.ToString());
            Assert.AreEqual("Bearer solver-key", handler.Requests[0].GetHeader("Authorization"));
        }

        [TestMethod]
        public async Task SolveAsyncUsesRequestScopedSolverHeaders()
        {
            var handler = new RecordingHttpMessageHandler();
            using var httpClient = new HttpClient(handler);
            var service = new DecisionRulesService(
                new DecisionRulesOptions("https://example.com", "solver-key"),
                httpClient);

            await service.SolveAsync(
                "rule-id",
                "{\"input\":\"first\"}",
                solverOptions: new SolverOptions.Builder()
                    .WithDebug(true)
                    .WithCorrId("first-correlation-id")
                    .WithAudit(true)
                    .WithAuditTtl(10)
                    .WithStrategy(StrategyOptions.ARRAY)
                    .WithLookupMethod(LookupMethodOptions.LOOKUP_EXISTS)
                    .Build());

            await service.SolveAsync(
                "rule-id",
                "{\"input\":\"second\"}",
                solverOptions: new SolverOptions.Builder()
                    .WithCorrId("second-correlation-id")
                    .WithLookupMethod(LookupMethodOptions.LOOKUP_VALUE)
                    .Build());

            Assert.AreEqual(2, handler.Requests.Count);

            Assert.AreEqual("true", handler.Requests[0].GetHeader("X-Debug"));
            Assert.AreEqual("first-correlation-id", handler.Requests[0].GetHeader("X-Correlation-Id"));
            Assert.AreEqual("true", handler.Requests[0].GetHeader("X-Audit"));
            Assert.AreEqual("10", handler.Requests[0].GetHeader("X-Audit-Ttl"));
            Assert.AreEqual("ARRAY", handler.Requests[0].GetHeader("X-Strategy"));
            Assert.AreEqual("LOOKUP_EXISTS", handler.Requests[0].GetHeader("X-Lookup-Method"));

            Assert.AreEqual("false", handler.Requests[1].GetHeader("X-Debug"));
            Assert.AreEqual("second-correlation-id", handler.Requests[1].GetHeader("X-Correlation-Id"));
            Assert.AreEqual("false", handler.Requests[1].GetHeader("X-Audit"));
            Assert.IsNull(handler.Requests[1].GetHeader("X-Audit-Ttl"));
            Assert.AreEqual("STANDARD", handler.Requests[1].GetHeader("X-Strategy"));
            Assert.AreEqual("LOOKUP_VALUE", handler.Requests[1].GetHeader("X-Lookup-Method"));
            Assert.IsNull(httpClient.DefaultRequestHeaders.Authorization);
            Assert.IsFalse(httpClient.DefaultRequestHeaders.Contains("X-Debug"));
            Assert.IsFalse(httpClient.DefaultRequestHeaders.Contains("X-Correlation-Id"));
        }

        [TestMethod]
        public void DefaultConstructorUsesSharedHttpClient()
        {
            var firstService = new DecisionRulesService(new DecisionRulesOptions("https://example.com", "solver-key"));
            var secondService = new DecisionRulesService(new DecisionRulesOptions("https://example.com", "solver-key"));

            var httpClientField = typeof(DecisionRulesService)
                .GetField("_httpClient", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(httpClientField);
            Assert.AreSame(
                httpClientField.GetValue(firstService),
                httpClientField.GetValue(secondService));
        }

        private sealed class RecordingHttpMessageHandler : HttpMessageHandler
        {
            public List<RecordedRequest> Requests { get; } = new List<RecordedRequest>();

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                string? content = request.Content == null
                    ? null
                    : await request.Content.ReadAsStringAsync(cancellationToken);

                var headers = request.Headers.ToDictionary(
                    header => header.Key,
                    header => header.Value.ToArray(),
                    StringComparer.OrdinalIgnoreCase);

                Requests.Add(new RecordedRequest(request.Method, request.RequestUri, headers, content));

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"ok\":true}")
                };
            }
        }

        private sealed class RecordedRequest
        {
            private readonly IReadOnlyDictionary<string, string[]> _headers;

            public RecordedRequest(
                HttpMethod method,
                Uri? requestUri,
                IReadOnlyDictionary<string, string[]> headers,
                string? content)
            {
                Method = method;
                RequestUri = requestUri;
                _headers = headers;
                Content = content;
            }

            public HttpMethod Method { get; }

            public Uri? RequestUri { get; }

            public string? Content { get; }

            public string? GetHeader(string name)
            {
                return _headers.TryGetValue(name, out var values)
                    ? string.Join(",", values)
                    : null;
            }
        }
    }
}
