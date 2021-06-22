using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Exceptions
{
    [Serializable]
    public class TooManyApiCallsException : Exception
    {
        public TooManyApiCallsException():base("Too many API calls!") { }
    }
}
