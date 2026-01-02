using DecisionRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules
{
    public interface IJobService
    {
        Task<Job> StartAsync(string ruleIdOrAlias, object inputData, int? version = null);
        Task<Job> CancelAsync(string jobId);
        Task<Job> GetInfoAsync(string jobId);

    }
}
