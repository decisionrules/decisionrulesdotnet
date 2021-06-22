using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Exceptions
{
    [Serializable]
    public class NotPublishedException : Exception
    {
        public NotPublishedException():base("Rule is not published!") { }
    }
}
