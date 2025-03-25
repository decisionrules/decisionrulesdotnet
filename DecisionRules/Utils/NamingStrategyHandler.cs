using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Utils
{
    /// <summary>
    /// Naming strategy handles serialization to .NET object
    /// </summary>
    internal class NamingStrategyHandler
    {
        public static JsonSerializerSettings CreateNamingStrategy(NamingStrategy namingStrategy)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = namingStrategy};
            return new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
            };
        }
    }
}
