using DecisionRules;
using DemoApp.Model;
using DemoApp.Model.Out;
using System;
using System.Collections.Generic;
using static DecisionRules.Model.GeoLocationsEnum;
using static DecisionRules.Model.ProtocolEnum;
using static DecisionRules.Model.SolverStragiesEnum;

namespace DemoApp
{
    class Program
    {

        private static readonly string apiKey = "API KEY";

        private static readonly string ruleId = "RULE ID";

        static void Main(string[] args)
        {
            CustomDomain customDomain = new CustomDomain("api.decisionrules.io", CustomDomainProtocol.HTTP);

            RequestOption requestOptions = new RequestOption(apiKey, GeoLocations.DEFAULT);

            DecisionRulesService drs = new DecisionRulesService(requestOptions);

            InputData inputModel = new InputData
            {
                day = "today"
            };

            string inputJSON = @"{""data"": {""day"":""today""}}";

            List<ResultModel> response = drs.Solve<InputData, ResultModel>(ruleId, inputModel, SolverStrategies.STANDARD).Result;

            List<ResultModel> responseString = drs.Solve<ResultModel>(ruleId, inputJSON, SolverStrategies.STANDARD).Result;

            Console.WriteLine(response[0].result);
            Console.WriteLine(responseString[0].result);
        }
    }

}
