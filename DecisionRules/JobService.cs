namespace DecisionRules
{
    using DecisionRules.Api;
    using DecisionRules.Models;
    using DecisionRules.Utilities;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    /// <summary>
    /// Provides access to the DecisionRules Job API endpoints.
    /// </summary>
    public class JobService : IJobService
    {
        private readonly DecisionRulesService _service;
        private readonly JobApi jobApi;

        internal JobService(DecisionRulesService service, HttpClient httpClient)
        {
            _service = service;
            jobApi = new JobApi(httpClient);
        }

        public virtual async Task<Job> StartAsync(string ruleIdOrAlias, object inputData, int? version = null)
        {
            try
            {
                return await jobApi.StartJobApiAsync(_service._options, ruleIdOrAlias, inputData, version);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public virtual async Task<Job> CancelAsync(string jobId)
        {
            try
            {
                return await jobApi.CancelJobApiAsync(_service._options, jobId);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public virtual async Task<Job> GetInfoAsync(string jobId)
        {
            try
            {
                return await jobApi.GetJobInfoApiAsync(_service._options, jobId);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }
    }
}
