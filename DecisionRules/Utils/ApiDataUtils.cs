using DecisionRules.Models;

namespace DecisionRules.Utils
{
    internal class ApiDataUtils
    {
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
