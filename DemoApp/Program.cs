using DecisionRules;
using DemoApp.Model;
using DemoApp.Model.Out;
using System;
using System.Collections.Generic;
using static DecisionRules.Model.GeoLocationsEnum;
using static DecisionRules.Model.ProtocolEnum;
using static DecisionRules.Model.SolverStragiesEnum;
using static DecisionRules.Model.SolverTypesEnum;

namespace DemoApp
{
    class Program
    {
      
        static void Main(string[] args)
        {
            CustomDomain customDomain = new CustomDomain("api.decisionrules.io", CustomDomainProtocol.HTTP);

            RequestOption requestOptions = new RequestOption(apiKey: Program.apiKey, publicApiKey: Program.publicApiKey, geoloc: GeoLocations.DEFAULT);

            DecisionRulesService solver = new DecisionRulesService(requestOptions);
            DecisionRulesPublicService crud = new DecisionRulesPublicService(requestOptions);

            Program.testSolvers(solver);

            Program.testCRUD(crud);
            
        }

        private static void testSolvers(DecisionRulesService solver)
        {
            InputData inputModel = new InputData
            {
                day = "today"
            };

            String inputJSON = @"{""data"": {""day"":""today""}}";

            List<ResultModel> solvedRule = solver.Solve<InputData, ResultModel>(Program.solveRule, inputModel, SolverStrategies.STANDARD, solverType: SolverTypeEnum.RULE).Result;

            Console.WriteLine("SOLVED RULE: " + solvedRule[0].result);

            List<ResultModel> solveComposition = solver.Solve<InputData, ResultModel>(Program.solveComposition, inputModel, SolverStrategies.STANDARD, solverType: SolverTypeEnum.COMPOSITION).Result;

            Console.WriteLine("SOLVED COMPOSITION: " + solveComposition[0].result);

            List<ResultModel> solvedRuleString = solver.Solve<ResultModel>(Program.solveRule, inputJSON, SolverStrategies.STANDARD, solverType: SolverTypeEnum.RULE).Result;

            Console.WriteLine("SOLVED RULE JSON STRING: " + solvedRuleString[0].result);

            List<ResultModel> solvedCompositionString = solver.Solve<ResultModel>(Program.solveComposition, inputJSON, SolverStrategies.STANDARD, solverType: SolverTypeEnum.COMPOSITION).Result;

            Console.WriteLine("SOLVED COMPOSITION JSON STRING: " + solvedCompositionString[0].result);
        }

        private static void testCRUD(DecisionRulesPublicService crud)
        {
            string postRuleString = @"{ ""name"": ""POSTRULE_C#"", ""description"": """", ""inputSchema"": { ""Input attribute"": {} }, ""outputSchema"": { ""Output Attribute"": {} }, ""decisionTable"": { ""columns"": [ { ""condition"": { ""type"": ""simple"", ""inputVariable"": ""Input attribute"", ""name"": ""New Condition"" }, ""columnId"": ""ec57bb7c-8e90-4aee-da49-17b607a6b09a"", ""type"": ""input"" }, { ""columnOutput"": { ""type"": ""simple"", ""outputVariable"": ""Output Attribute"", ""name"": ""New Result"" }, ""columnId"": ""2e46eb73-de05-51bc-5913-4b261bbe2069"", ""type"": ""output"" } ], ""rows"": [ { ""cells"": [ { ""column"": ""ec57bb7c-8e90-4aee-da49-17b607a6b09a"", ""scalarCondition"": { ""value"": """", ""operator"": ""anything"" }, ""type"": ""input"" }, { ""column"": ""2e46eb73-de05-51bc-5913-4b261bbe2069"", ""outputScalarValue"": { ""type"": ""common"", ""value"": ""Hello from Solver"" }, ""type"": ""output"" } ] } ] }, ""type"": ""decision-table"", ""status"": ""published"", ""createdIn"": ""2021-09-23T08:14:07.852Z"", ""lastUpdate"": ""2021-09-23T08:14:07.852Z"" }";
            string putRuleString = @"{ ""name"": ""PUTRULE_C#"", ""description"": """", ""inputSchema"": { ""Input attribute"": {} }, ""outputSchema"": { ""Output Attribute"": {} }, ""decisionTable"": { ""columns"": [ { ""condition"": { ""type"": ""simple"", ""inputVariable"": ""Input attribute"", ""name"": ""New Condition"" }, ""columnId"": ""ec57bb7c-8e90-4aee-da49-17b607a6b09a"", ""type"": ""input"" }, { ""columnOutput"": { ""type"": ""simple"", ""outputVariable"": ""Output Attribute"", ""name"": ""New Result"" }, ""columnId"": ""2e46eb73-de05-51bc-5913-4b261bbe2069"", ""type"": ""output"" } ], ""rows"": [ { ""cells"": [ { ""column"": ""ec57bb7c-8e90-4aee-da49-17b607a6b09a"", ""scalarCondition"": { ""value"": """", ""operator"": ""anything"" }, ""type"": ""input"" }, { ""column"": ""2e46eb73-de05-51bc-5913-4b261bbe2069"", ""outputScalarValue"": { ""type"": ""common"", ""value"": ""Hello from Solver"" }, ""type"": ""output"" } ] } ] }, ""type"": ""decision-table"", ""status"": ""published"", ""createdIn"": ""2021-09-23T08:14:07.852Z"", ""lastUpdate"": ""2021-09-23T08:14:07.852Z"" }";

            var getRuleId = crud.GetRuleById(Program.getRule).Result;
            var getRuleIdAndVersion = crud.GetRuleByIdAndVersion(Program.getRule, "1").Result;
            var getSpaceInfo = crud.GetSpaceInfo(Program.getSpace).Result;

            var postRule = crud.PostRuleForSpace(Program.getSpace, postRuleString).Result;
            var putRule = crud.PutRule(Program.getRule, "1", putRuleString).Result;

            var deleteRule = crud.DeleteRule(Program.deleteRule, "1").Result;

            Console.WriteLine("GET RULE: " + getRuleId);
            Console.WriteLine("GET RULE AND VER: " + getRuleIdAndVersion);
            Console.WriteLine("GET SPACE INFO: " + getSpaceInfo);

        }
    }

}
