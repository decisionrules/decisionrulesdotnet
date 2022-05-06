namespace DecisionRules
{
    public class CustomDomain
    {
        private  string _domain;
        private  Enums.Protocol _protocol;
        private  int _port;

        public CustomDomain(string domain, Enums.Protocol protocol) : this(domain, protocol, 0) { }

        public CustomDomain(string domain, Enums.Protocol protocol, int port)
        {
            _domain = domain;
            _protocol = protocol;
            if (port == 0)
            {
                switch (protocol)
                {
                    case Enums.Protocol.HTTP: _port = 80; break;
                    case Enums.Protocol.HTTPS: _port = 443; break;
                }
            } else
            {
                _port = port;
            }
            
        }

        public string CreateSolverUrl(Enums.SolverMode solverMode)
        {
            return $"{_protocol.ToString().ToLower()}://{_domain}:{_port}/{solverMode}/solve";
        }

        public string CreateManagementUrl()
        {
            return $"{_protocol.ToString().ToLower()}://{_domain}:{_port}/api";
        }
    }
}
