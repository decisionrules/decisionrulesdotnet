using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Exceptions
{
    [Serializable]
    public class TooManyApiCallsException : Exception
    {
        public TooManyApiCallsException():base("ERR: Too many API calls") { }
    }
}
