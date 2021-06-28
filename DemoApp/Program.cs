using DecisionRules;
using DemoApp.Model;
using DemoApp.Model.Out;
using System;
using System.Collections.Generic;
using static DecisionRules.Model.GeoLocationsEnum;
using static DecisionRules.Model.SolverStragiesEnum;

namespace DemoApp
{
    class Program
    {

        private static readonly string apiKey = "API_KEY_HERE";

        private static readonly string ruleId = "RULE_ID_HERE";

        static void Main(string[] args)
        {

            RequestOption requestOptions = new RequestOption(apiKey, GeoLocations.US2);

            DecisionRulesService drs = new DecisionRulesService(requestOptions);

            InputData inputModel = new InputData
            {
                day = "today"
            };

            string inputJSON = @"{""data"": {""day"":""today""}}";

            List<ResultModel> response = drs.Solve<InputData, ResultModel>(ruleId, inputModel, SolverStrategies.STANDARD, "VERSION_HERE").Result;

            List<ResultModel> responseString = drs.Solve<ResultModel>(ruleId, inputJSON, SolverStrategies.STANDARD, "VERSION_HERE").Result;

            Console.WriteLine(response[0].result);
            Console.WriteLine(responseString[0].result);
        }
    }

}
