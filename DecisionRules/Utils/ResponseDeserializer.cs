using DecisionRules.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DecisionRules.Utils
{
    internal class ResponseDeserializer
    {
        public async static Task<List<T>> DeserializeSolverResponse<T>(HttpResponseMessage response)
        {
            try
            {
                
                return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
            } catch
            {
                string responseValue = await response.Content.ReadAsStringAsync();
                throw new FailedToSolveRuleException(responseValue);
            }
        }
    }
}
