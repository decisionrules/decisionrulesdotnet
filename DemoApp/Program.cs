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

        private static readonly string apiKey = "";

        private static readonly string publicApiKey = "";

        private static readonly string ruleId = "";
        private static readonly string spaceId = "";

        private static readonly string ruleIdToDelete = "";

        static void Main(string[] args)
        {
            CustomDomain customDomain = new CustomDomain("api.decisionrules.io", CustomDomainProtocol.HTTP);

            RequestOption requestOptions = new RequestOption(apiKey: apiKey, publicApiKey: publicApiKey, geoloc:GeoLocations.DEFAULT);

            DecisionRulesService drs = new DecisionRulesService(requestOptions);

            DecisionRulesPublicService drsp = new DecisionRulesPublicService(requestOptions);

            InputData inputModel = new InputData
            {
                day = "today"
            };

            string inputJSON = @"{""data"": {""day"":""today""}}";

            //List<ResultModel> response = drs.Solve<InputData, ResultModel>(ruleId, inputModel, SolverStrategies.STANDARD, solverType:SolverTypeEnum.COMPOSITION).Result;

            //List<ResultModel> responseString = drs.Solve<ResultModel>(ruleId, inputJSON, SolverStrategies.STANDARD, solverType:SolverTypeEnum.COMPOSITION).Result;

            string postIntpu = @"{ ""name"": ""POSTRULE_PUT"", ""description"": """", ""inputSchema"": { ""Input attribute"": {} }, ""outputSchema"": { ""Output Attribute"": {} }, ""decisionTable"": { ""columns"": [ { ""condition"": { ""type"": ""simple"", ""inputVariable"": ""Input attribute"", ""name"": ""New Condition"" }, ""columnId"": ""ec57bb7c-8e90-4aee-da49-17b607a6b09a"", ""type"": ""input"" }, { ""columnOutput"": { ""type"": ""simple"", ""outputVariable"": ""Output Attribute"", ""name"": ""New Result"" }, ""columnId"": ""2e46eb73-de05-51bc-5913-4b261bbe2069"", ""type"": ""output"" } ], ""rows"": [ { ""cells"": [ { ""column"": ""ec57bb7c-8e90-4aee-da49-17b607a6b09a"", ""scalarCondition"": { ""value"": """", ""operator"": ""anything"" }, ""type"": ""input"" }, { ""column"": ""2e46eb73-de05-51bc-5913-4b261bbe2069"", ""outputScalarValue"": { ""type"": ""common"", ""value"": ""Hello from Solver"" }, ""type"": ""output"" } ] } ] }, ""type"": ""decision-table"", ""status"": ""published"", ""createdIn"": ""2021-09-23T08:14:07.852Z"", ""lastUpdate"": ""2021-09-23T08:14:07.852Z"" }";

            dynamic postRuleForSpace = drsp.PutRule<dynamic>(ruleId, "1", postIntpu).Result;

            Console.WriteLine(postRuleForSpace);

            //Console.WriteLine(response[0].result);
            //Console.WriteLine(responseString[0].result);
        }
    }

    class PostInput
    {

    }

}
