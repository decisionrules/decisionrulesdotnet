using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Exceptions
{
    [Serializable]
    public class NoUserException : Exception
    {
        public NoUserException():base("ERR: No user.") { }
    }
}
