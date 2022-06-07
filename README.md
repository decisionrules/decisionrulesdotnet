# Summary
[Decisionrules.io](https://decisionrules.io/) library that allows you to integrate DecisionRules Solver and Management API to you application as easily as possible. SDK allow you to solve all rule types that are available, CRUD operations on all rule types, rules status management and rule tags management.
> VERSION 3 IS NEW MAJOR VERSION OF THIS SDK AND IT IS STRONGLY RECOMMENDED, DUE TO DEPRECATION OF OLDER VERSIONS.

SDK v3 is supported for [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) and [.NET STANDARD 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-1-0) and it is highly recommended to target `.net 6.0`.
# Installation
You can simply integrate [SDK](https://www.nuget.org/packages/DecisionRules/) to your project via NuGet package manager.
> Please note that SDK uses [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) library for serialization and deserialization of request / response data.
# Defining Custom domain
Custom domain is special class that is designed for those who uses DecisionRules in private cloud or as on-premise.

Domain argument is name of desired domain, protocol is HTTP or HTTPS and port is TCP/IP port.
If port is not defined in the class constructor it is set to default value by protocol value, 80 for HTTP and 443 for HTTPS.
```csharp
CustomDomain customDomain = new CustomDomain("api.mydomain.com", HTTPS, 443);
```
# Using Solver API
Solver class takes up to 3 arguments that are `api key`(can be generated on dashboard), `custom domain` object and Newtonsoft `naming strategy` for JSON serialization. Last two arguments are not mandatory and are set to default values on init. 
Class exposes two async methods: SolveRule and SolveRuleFlow.
```csharp
public async Task<List<SampleResponse>> amazingRuleSolver() 
{
	Solver solver = new Solver("myApiKey");
	
	string itemId = "id of a rule that is being solved";
	string itemId2 = "id of a ruleflow that is being solved";
	
	SampleRequest request = new();
	request.InputAttribute = "MY RULE INPUT";

	SampleResponse response = new();
	
	SampleResponse resultForRule = await solver.SolveRule<SampleRequest,SampleResponse>(itemId, data);
	SampleResponse resultForRuleFlow = await solver.SolverRuleFlow<SampleRequest,SampleResponse>(itemId2, data);
	
	return resultForRule;
}

class SampleRequest
{
	public string InputAtrribute { get; set;}
}

class SampleResponse
{
	pubic string OutputAttribute { get; set;}
}
```
# Using Management API
Management class takes on argument, management api key. Class exposes number of methods listed below.

 - GetRule - get rule by itemId and version*
 - CreateRule - create rule by spaceId and ruleData
 - UpdateRule - updates rule by itemId, newRuleData and version*
 - DeleteRule - deletes rule by itemId and version
 - GetSpaceItems - get space items that belongs to management api key or get items by tags 
 - GetRuleFlow - get rule by itemId and version*
 - CreateRuleFlow - create ruleflow in space that belongs to management api key
 - UpdateRuleFlow - updates ruleflowby itemId, newRuleflowData and version*
 - DeleteRuleFlow - deletes ruleflow by itemId and version
 - ExportRuleFlow - exports ruleflow by itemId and version*
 - ImportRuleFlow - import ruleflow as a new ruleflow or new version of existing ruleflow or override existing ruleflow.
 - ChangeRuleStatus - changes rule status
 - ChangeRuleFlowStatus - changes ruleflow status
 - UpdateTags - update tags on rule or ruleflow
 - DeleteTags - delete tags on rule or ruleflow

 > \* = optional argument

## Example usage
```csharp
public async Task<Object> manageRules()
{
	Management manager = new Management("management_key");
	
	string itemId = "some rule or ruleflow id"

	return await manager.GetRule(itemId);
} 
```