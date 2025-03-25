using DecisionRules.Models;

namespace DecisionRules.Utils
{
    internal class ApiDataUtils
    {
        /// <summary>
        /// Wraps input data to data object for DecisionRules
        /// </summary>
        /// <param name="userData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ApiDataWrapper<T> PrepareRequest<T>(T userData)
        {
            ApiDataWrapper<T> request = new ApiDataWrapper<T>
            {
                Data = userData
            };

            return request;
        }
    }
}
