using System;

namespace DecisionRules.Exceptions
{
    public class FailedToSolveRuleException : Exception
    {
        public FailedToSolveRuleException() { }
        public FailedToSolveRuleException(string message) : base(message) { }

        public FailedToSolveRuleException(string message, Exception inner): base(message, inner) { }
    }
}
