using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Utils
{
    internal class SolverUtils
    {
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
