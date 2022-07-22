using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Utils
{
    internal class NamingStrategyHandler
    {
        public static JsonSerializerSettings CreateNamingStrategy(NamingStrategy namingStrategy)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            };
        }
    }
}
