using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules
{
    class RequestModel<T>
    {
        public T data { get; set; }

        public RequestModel() { }

        public RequestModel(T inputData)
        {
            this.data = inputData;
        }

    }
}
