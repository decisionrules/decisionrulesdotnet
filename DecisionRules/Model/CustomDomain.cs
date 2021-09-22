using System;
using System.Collections.Generic;
using System.Text;
using static DecisionRules.Model.ProtocolEnum;

namespace DecisionRules
{
    public class CustomDomain
    {
        private string customDomainUrl;
        private CustomDomainProtocol customDomainProtocol;

        public CustomDomain(string customDomainUrl, CustomDomainProtocol customDomainProtocol)
        {
            this.customDomainUrl = customDomainUrl;
            this.customDomainProtocol = customDomainProtocol;
        }

        public string CustomDomainUrl
        {
            get => customDomainUrl;
        }

        public CustomDomainProtocol CustomDomainProtocol
        {
            get => customDomainProtocol;
        }
    }
}
