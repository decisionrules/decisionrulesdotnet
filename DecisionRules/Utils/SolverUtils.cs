using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Utils
{
    internal class SolverUtils
    {
        /// <summary>
        /// Resolves right URL format for given version
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string SetRuleIdAndVersion(string id, int version)
        {
            if (version > 0)
            {
                return $"/{id}/{version}";
            }

            return $"/{id}/";
        }
    }
}
