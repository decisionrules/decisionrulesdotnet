namespace DecisionRules
{
    public class Enums
    {
        public enum Protocol
        {
            HTTP,
            HTTPS 
        }

        public enum RuleStrategy
        {
            STANDARD,
            ARRAY,
            FIRST_MATCH,
            EVALUATE_ALL
        }

        public enum SolverMode
        {
            RULE,
            COMPOSITION
        }

        public enum RuleFlowStrategy
        {
            STANDARD,
            ARRAY,
            FIRST_MATCH,
        }

    }
}
