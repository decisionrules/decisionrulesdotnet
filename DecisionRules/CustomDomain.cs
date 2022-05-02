using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules
{
    public class CustomDomain
    {
        private readonly string _domain;
        private readonly Enums.Protocol _protocol;

        public CustomDomain(string domain, Enums.Protocol protocol)
        {
            _domain = domain;
            _protocol = protocol;
        }

        public string Domain
        {
            get { return _domain; }
        }

        public string Protocol
        {
            get { return _protocol.Equals(Enums.Protocol.HTTP) ? "http" : "https"; }
        }
    }
}
