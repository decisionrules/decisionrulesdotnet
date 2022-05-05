using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules
{
    public class Url
    {
        private CustomDomain _customDomain;

        public Url(CustomDomain customDomain)
        {
            _customDomain = customDomain;
        }

        public string createSolverUrl(Enums.SolverMode mode)
        {
            string smode = mode.Equals(Enums.SolverMode.RULE) ? "rule" : "composition";

            if (_customDomain == null)
            {
                return $"https://api.decisionrules.io/{smode}/solve";
            } 

            return $"{_customDomain.Protocol}://{this._customDomain.Domain}/{smode}/solve";
            
        }

        public string createManagementUrl()
        {
            if (_customDomain == null)
            {
                return "https://api.decisionrules.io/api";
            }

            string stringProtocol = this._customDomain.Protocol;

            return $"{stringProtocol}://{this._customDomain.Domain}/api";
        }
    }
}
