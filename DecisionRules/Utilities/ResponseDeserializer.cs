using DecisionRules.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules.Utilities
{
    internal class ResponseDeserializer
    {
        /// <summary>
        /// Deserializes HTTP Response from DecisionRules API server
        /// </summary>
        /// <param name="response"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="FailedToSolveRuleException"></exception>
        public async static Task<List<T>> DeserializeSolverResponse<T>(HttpResponseMessage response)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                string responseValue = await response.Content.ReadAsStringAsync();
                throw new DecisionRulesErrorException(responseValue);
            }   
        }
    }
}
