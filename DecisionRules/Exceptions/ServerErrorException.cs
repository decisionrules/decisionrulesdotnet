using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Exceptions
{
    [Serializable]
    public class ServerErrorException : Exception
    {
        public ServerErrorException():base("Server side problem") { }
    }
}
