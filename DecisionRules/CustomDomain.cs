namespace DecisionRules
{
    /// <summary>
    /// Sets Custom domain for use of on-premise OR private managed cloud environemnts
    /// </summary>
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

        /// <summary>
        /// Created DecisionRules Solver API URL
        /// </summary>
        /// <param name="solverMode"></param>
        /// <returns></returns>
        public string CreateSolverUrl(Enums.SolverMode solverMode)
        {
            return $"{_protocol.ToString().ToLower()}://{_domain}:{_port}/{solverMode}/solve";
        }

        /// <summary>
        /// Created DecisionRules Management API url
        /// </summary>
        /// <returns></returns>
        public string CreateManagementUrl()
        {
            return $"{_protocol.ToString().ToLower()}://{_domain}:{_port}/api";
        }
    }
}
