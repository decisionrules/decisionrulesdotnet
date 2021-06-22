using DecisionRules;
using DemoApp.Model;
using DemoApp.Model.Out;
using System;
using System.Collections.Generic;

namespace DemoApp
{
    class Program
    {

        private static readonly string apiKey = "API_KEY_HERE";

        private static readonly string ruleId = "RULE_ID_HERE";

        static void Main(string[] args)
        {
            RequestOption requestOptions = new RequestOption(apiKey, "eu1");

            DecisionRulesService drs = new DecisionRulesService(requestOptions);

            InputData inputModel = new InputData
            {
                day = "today"
            };

            string inputJSON = @"{""data"": {""day"":""today""}}";

            List<ResultModel> response = drs.Solve<InputData, ResultModel>(ruleId, inputModel).Result;

            List<ResultModel> responseString = drs.Solve<ResultModel>(ruleId, inputJSON, "1").Result;

            Console.WriteLine(response[0].result);
            Console.WriteLine(responseString[0].result);
        }
    }

}
