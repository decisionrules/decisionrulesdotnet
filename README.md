Simple library that allows you to easily connect to [Decisionrules.io](https://decisionrules.io/) from your application.

# Request Options

* API_KEY - [Your API_KEY](https://docs.decisionrules.io/docs/api/api-keys) (string)
* Geolocation - optional argument. If omited default geoloc is set. [Our geolocs](https://docs.decisionrules.io/docs/api/geo-location) (string)
* URI - Custom base url for on premise version of DecisionRules (if omited our default uri is used (api.decisionrules.io)); (string)

# Arguments

* U - generic return type
* T - generic input argument
* JsonStringInput - input model in json string format instead of T type. (string)
* Version - optional parameter that defines which version of rule you want use in solver API. If omited last version is used. (string)

# NuGetRepository
* https://www.nuget.org/packages/DecisionRules/


## Usage

```c#
namespace DemoApp
{
    class Program
    {

        private static readonly string apiKey = "API_KEY_HERE";

        private static readonly string ruleId = "RULE_ID_HERE";

        static void Main(string[] args)
        {
            RequestOption requestOptions = new RequestOption(apiKey, GeoLocations.DEFAULT, "IF ONPROMISE version then base url here");

            DecisionRulesService drs = new DecisionRulesService(requestOptions);

            InputData inputModel = new InputData
            {
                day = "today"
            };

            string inputJSON = @"{""data"": {""day"":""today""}}";

            List<ResultModel> response = drs.Solve<InputData, ResultModel>(ruleId, inputModel, SolverStrategies.STANDARD,"VERSION_HERE").Result;

            List<ResultModel> responseString = drs.Solve<ResultModel>(ruleId, inputJSON, SolverStrategies.STANDARD,"VERSION_HERE").Result;

            Console.WriteLine(response[0].result);
            Console.WriteLine(responseString[0].result);
        }
    }

}
```
### InputModel preview example

```c#
class InputModel
{
    public string day { get; set; }
}
```

### ResultModel preview example

```c#
class ResultModel
{
    public string result { get; set; }
}
```
