using System;

namespace DecisionRules.Exceptions
{
    /// <summary>
    /// Custom exception for errors reported by the DecisionRules API or client.
    /// </summary>
    public class DecisionRulesErrorException : Exception
    {
        /// <summary>
        /// Stores the string representation of the stack trace, as seen in the Java code.
        /// </summary>
        public string StackTraceString { get; }

        /// <summary>
        /// Constructor matching the Java code's usage in Utils.handleError.
        /// </summary>
        public DecisionRulesErrorException(string message, string stackTrace) : base(message)
        {
            StackTraceString = stackTrace;
        }

        /// <summary>
        /// A more idiomatic C# constructor that accepts an inner exception.
        /// </summary>
        public DecisionRulesErrorException(string message, Exception innerException) : base(message, innerException)
        {
            StackTraceString = innerException?.ToString();
        }
    }
}