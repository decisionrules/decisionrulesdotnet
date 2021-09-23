using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionRules.Exceptions
{
    [Serializable]
    public class InvalidSolverTypeException : Exception
    {
        public InvalidSolverTypeException() : base("ERR: Invalid solver type") { }
    }
}
