namespace DecisionRules
{
    public class CustomDomain
    {
        private  string _domain;
        private  Enums.Protocol _protocol;
        private  int _port;

        public CustomDomain(string domain, Enums.Protocol protocol, int port)
        {
            _domain = domain;
            _protocol = protocol;
            _port = port;
            
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
