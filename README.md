# DecisionRules for .NET

A [Decisionrules.io](https://decisionrules.io) library that allows you to integrate the DecisionRules Solver, Management, and Job APIs into your .NET application as easily as possible.

---

## Table of contents
1.  [Migration Guide from v3 to v4](#migration-guide-from-v3-to-v4)
2.  [Usage and Examples](#usage-and-examples)
3.  [API](#api)

---


## Migration Guide from v3 to v4

If you're upgrading from DecisionRules .NET SDK v3 to v4, here are the key changes you need to know:

### Breaking Changes

#### 1. **Initialization and Configuration**

**v3:**
```csharp
Solver solver = new Solver("myApiKey");
Management manager = new Management("management_key");
```

**v4:**
```csharp
var options = new DecisionRulesOptions("YOUR_HOST", "YOUR_SOLVER_KEY", "YOUR_MANAGEMENT_KEY");
var dr = new DecisionRulesService(options);
```

**Key Differences:**
- v4 uses a unified `DecisionRulesService` class instead of separate `Solver` and `Management` classes
- Configuration is now centralized through `DecisionRulesOptions`
- Host URL must be explicitly provided
- Both Solver and Management keys are configured together

#### 2. **Custom Domain Configuration**

**v3:**
```csharp
CustomDomain customDomain = new CustomDomain("api.mydomain.com", HTTPS, 443);
Solver solver = new Solver("myApiKey", customDomain);
```

**v4:**
```csharp
var options = new DecisionRulesOptions("https://api.mydomain.com:443", "SOLVER_KEY", "MANAGEMENT_KEY");
var dr = new DecisionRulesService(options);
```

**Key Differences:**
- Custom domain configuration is now part of the host URL string
- No separate `CustomDomain` class needed

#### 3. **Solver API Methods**

**v3:**
```csharp
List<SampleResponse> resultForRule = await solver.SolveRule<SampleRequest, SampleResponse>(itemId, data);
List<SampleResponse> resultForRuleFlow = await solver.SolveRuleFlow<SampleRequest, SampleResponse>(itemId2, data);
```

**v4:**
```csharp
string resultJson = await dr.SolveAsync(ruleIdOrAlias, inputData);
// Parse JSON manually
var result = JsonSerializer.Deserialize<List<SampleResponse>>(resultJson);
```

**Key Differences:**
- v4 returns raw JSON string instead of strongly-typed generic results
- No distinction between `SolveRule` and `SolveRuleFlow` - use single `SolveAsync` method
- Manual JSON deserialization required (uses `System.Text.Json` instead of `Newtonsoft.Json`)
- Optional `version` parameter available

#### 4. **Management API Access**

**v3:**
```csharp
Management manager = new Management("management_key");
var rule = await manager.GetRule(itemId);
```

**v4:**
```csharp
var dr = new DecisionRulesService(options);
var rule = await dr.Management.GetRuleAsync(itemId);
```

**Key Differences:**
- Management API accessed via `dr.Management` property
- All methods now have `Async` suffix following .NET conventions
- Method names use PascalCase consistently

#### 5. **Method Name Changes and New Methods**

| **v3 Method** | **v4 Method** | **Notes** |
|:---|:---|:---|
| `SolveRule<TRequest, TResponse>(itemId, data)` | `SolveAsync(ruleIdOrAlias, inputData, version)` | Returns raw JSON string |
| `SolveRuleFlow<TRequest, TResponse>(itemId, data)` | `SolveAsync(ruleIdOrAlias, inputData, version)` | Unified with SolveRule |
| `GetRule(itemId, version)` | `GetRuleAsync(ruleIdOrAlias, version)` | |
| *(not available)* | `GetRuleByPathAsync(path, version)` | **New in v4** |
| `CreateRule(spaceId, ruleData)` | `CreateRuleAsync(rule, path)` | |
| *(not available)* | `CreateNewRuleVersionAsync(ruleIdOrAlias, rule)` | **New in v4** |
| `UpdateRule(itemId, newRuleData, version)` | `UpdateRuleAsync(ruleIdOrAlias, rule, version)` | |
| `DeleteRule(itemId, version)` | `DeleteRuleAsync(ruleIdOrAlias, version)` | |
| *(not available)* | `DeleteRuleByPathAsync(path, version)` | **New in v4** |
| `GetSpaceItems()` | `GetRulesForSpaceAsync()` | |
| *(not available)* | `GetRulesByTagsAsync(tags)` | **New in v4** |
| `GetRuleFlow(itemId, version)` | `GetRuleAsync(ruleIdOrAlias, version)` | Unified with GetRule |
| `CreateRuleFlow(data)` | `CreateRuleAsync(rule, path)` | Unified with CreateRule |
| `UpdateRuleFlow(itemId, data, version)` | `UpdateRuleAsync(ruleIdOrAlias, rule, version)` | Unified with UpdateRule |
| `DeleteRuleFlow(itemId, version)` | `DeleteRuleAsync(ruleIdOrAlias, version)` | Unified with DeleteRule |
| `ExportRuleFlow(itemId, version)` | `ExportFolderAsync(nodeId)` | |
| *(not available)* | `ExportFolderByPathAsync(path)` | **New in v4** |
| `ImportRuleFlow(data)` | `ImportFolderAsync(targetNodeId, data)` | |
| *(not available)* | `ImportFolderToPathAsync(path, data)` | **New in v4** |
| `ChangeRuleStatus(itemId, status, version)` | `UpdateRuleStatusAsync(ruleIdOrAlias, status, version)` | |
| `ChangeRuleFlowStatus(itemId, status, version)` | `UpdateRuleStatusAsync(ruleIdOrAlias, status, version)` | Unified with ChangeRuleStatus |
| `UpdateTags(itemId, tags, version)` | `UpdateTagsAsync(ruleIdOrAlias, tags, version)` | |
| `DeleteTags(itemId, tags, version)` | `DeleteTagsAsync(ruleIdOrAlias, tags, version)` | |
| *(not available)* | `LockRuleAsync(ruleIdOrAlias, lock, version)` | **New in v4** |
| *(not available)* | `LockRuleByPathAsync(path, lock, version)` | **New in v4** |
| *(not available)* | `FindDuplicatesAsync(ruleIdOrAlias, version)` | **New in v4** |
| *(not available)* | `FindDependenciesAsync(ruleIdOrAlias, version)` | **New in v4** |
| *(not available)* | `CreateFolderAsync(targetNodeId, data)` | **New in v4** |
| *(not available)* | `CreateFolderByPathAsync(path, data)` | **New in v4** |
| *(not available)* | `UpdateNodeFolderStructureAsync(targetNodeId, data)` | **New in v4** |
| *(not available)* | `UpdateNodeFolderStructureByPathAsync(path, data)` | **New in v4** |
| *(not available)* | `GetFolderStructureAsync(targetNodeId)` | **New in v4** |
| *(not available)* | `GetFolderStructureByPathAsync(path)` | **New in v4** |
| *(not available)* | `DeleteFolderAsync(targetNodeId, deleteAll)` | **New in v4** |
| *(not available)* | `DeleteFolderByPathAsync(path, deleteAll)` | **New in v4** |
| *(not available)* | `RenameFolderAsync(targetNodeId, newName)` | **New in v4** |
| *(not available)* | `RenameFolderByPathAsync(path, newName)` | **New in v4** |
| *(not available)* | `MoveFolderAsync(targetId, nodes, targetPath)` | **New in v4** |
| *(not available)* | `FindFolderOrRuleByAttributeAsync(data)` | **New in v4** |
| *(not available)* | **Job API - `StartAsync(ruleIdOrAlias, inputData, version)`** | **New in v4** |
| *(not available)* | **Job API - `CancelAsync(jobId)`** | **New in v4** |
| *(not available)* | **Job API - `GetInfoAsync(jobId)`** | **New in v4** |

#### 6. **New Features in v4**

v4 introduces several new features not available in v3:

- **Path-based operations**: Methods like `GetRuleByPathAsync`, `CreateFolderByPathAsync`, etc.
- **Job API**: Complete async job execution support via `dr.Job`
- **Folder management**: Comprehensive folder structure operations
- **Lock/Unlock rules**: `LockRuleAsync` and `LockRuleByPathAsync`
- **Find duplicates**: `FindDuplicatesAsync`
- **Find dependencies**: `FindDependenciesAsync`
- **Advanced search**: `FindFolderOrRuleByAttributeAsync`

#### 7. **Serialization Changes**

**v3:**
- Used `Newtonsoft.Json` for serialization
- Custom naming strategy support
- Automatic camelCase conversion

**v4:**
- Uses `System.Text.Json` by default
- Manual JSON parsing required for Solver API results
- More control over serialization/deserialization

### Migration Example

**Complete v3 Example:**
```csharp
// v3 Implementation
Solver solver = new Solver("myApiKey");
Management manager = new Management("management_key");

SampleRequest request = new SampleRequest { InputAttribute = "MY RULE INPUT" };
List<SampleResponse> result = await solver.SolveRule<SampleRequest, SampleResponse>(itemId, request);

var rule = await manager.GetRule(itemId);
await manager.ChangeRuleStatus(itemId, RuleStatus.PUBLISHED, 1);
```

**Equivalent v4 Example:**
```csharp
// v4 Implementation
var options = new DecisionRulesOptions("https://api.decisionrules.io", "myApiKey", "management_key");
var dr = new DecisionRulesService(options);

var request = new { InputAttribute = "MY RULE INPUT" };
string resultJson = await dr.SolveAsync(itemId, request);
var result = JsonSerializer.Deserialize<List<SampleResponse>>(resultJson);

var rule = await dr.Management.GetRuleAsync(itemId);
await dr.Management.UpdateRuleStatusAsync(itemId, RuleStatus.PUBLISHED, 1);
```

### Migration Checklist

- [ ] Update NuGet package to v4
- [ ] Replace `Solver` and `Management` classes with `DecisionRulesService`
- [ ] Update configuration to use `DecisionRulesOptions`
- [ ] Add `Async` suffix to all method calls
- [ ] Update method names according to the mapping table
- [ ] Handle JSON deserialization manually for Solver API results
- [ ] Replace `Newtonsoft.Json` references with `System.Text.Json` (if needed)
- [ ] Update `CustomDomain` usage to use host URL string
- [ ] Test all rule solving and management operations


---


## Usage and Examples

You can start using the library by creating a `DecisionRulesService` instance and providing valid `DecisionRulesOptions`.

### Initialization Example

```csharp
using DecisionRules;

// Configure options with your credentials
var options = new DecisionRulesOptions("YOUR_HOST", "YOUR_SOLVER_KEY", "YOUR_MANAGEMENT_KEY");

// Initialize the service
var dr = new DecisionRulesService(options);
```

### Solver API Call Example

Calls can be made with the top-level `SolveAsync` method. It returns a raw JSON string for you to process.

```csharp
using System.Collections.Generic;
using System.Text.Json;

// Prepare input data (can be any serializable object)
var inputData = new Dictionary<string, object>
{
    ["tripDetails"] = new Dictionary<string, object>
    {
        ["origin"] = "ATL",
        ["destination"] = "DXB"
    }
};

// Solve the rule and get the raw JSON string result
string resultJson = await dr.SolveAsync("ruleIdOrAlias", inputData);

// You can then parse the JSON string as needed
var result = JsonSerializer.Deserialize<Dictionary<string, object>>(resultJson);
```

### Management API Example

The Management API can be used through the `Management` object on the `DecisionRulesService` instance.

```csharp
using DecisionRules.Models;

// Get a rule by its ID
Rule rule = await dr.Management.GetRuleAsync("ruleIdOrAlias");
```

### Job API Example

The Job API is used to run input data against an Integration Flow asynchronously.

```csharp
using DecisionRules.Models;
using System.Collections.Generic;

// Prepare input data
var inputData = new Dictionary<string, object>
{
    ["tripDetails"] = new Dictionary<string, object>
    {
        ["origin"] = "ATL",
        ["destination"] = "DXB"
    }
};

// Start the job
Job job = await dr.Job.StartAsync("integrationFlowIdOrAlias", inputData);
```

---

## API

All methods described below are exposed on the `DecisionRulesService` class.

### Solver API

#### **DecisionRules.SolveAsync**

Solves a rule with the given input data and returns a raw JSON string as the result.

```csharp
string result = await dr.SolveAsync(ruleIdOrAlias, inputData);
string result = await dr.SolveAsync(ruleIdOrAlias, inputData, version);
```

**Arguments:**

| **arg** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `inputData` | `object` | no |
| `version` | `int?` | yes |

---

### Management API

All management methods are available under the `dr.Management` object.

#### **DecisionRules.Management.GetRuleAsync / GetRuleByPathAsync**

Gets all information about a rule. If version is not specified, it gets the latest published version.

```csharp
Rule result = await dr.Management.GetRuleAsync(ruleIdOrAlias, version);
Rule result = await dr.Management.GetRuleByPathAsync(path, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` / `path` | `string` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.UpdateRuleStatusAsync**

Changes a rule's status (e.g., from `PENDING` to `PUBLISHED`).

```csharp
Rule result = await dr.Management.UpdateRuleStatusAsync(ruleIdOrAlias, status, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `status` | `RuleStatus` | no |
| `version` | `int` | no |

#### **DecisionRules.Management.UpdateRuleAsync**

Changes a rule according to the provided `Rule` object.

```csharp
Rule result = await dr.Management.UpdateRuleAsync(ruleIdOrAlias, rule, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `rule` | `Rule` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.CreateRuleAsync**

Creates a new rule based on the provided `Rule` object.

```csharp
Rule result = await dr.Management.CreateRuleAsync(rule, path);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `rule` | `Rule` | no |
| `path` | `string` | yes |

#### **DecisionRules.Management.CreateNewRuleVersionAsync**

Creates a new version of an existing rule.

```csharp
Rule result = await dr.Management.CreateNewRuleVersionAsync(ruleIdOrAlias, rule);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `rule` | `Rule` | no |

#### **DecisionRules.Management.DeleteRuleAsync / DeleteRuleByPathAsync**

Deletes a rule.

```csharp
await dr.Management.DeleteRuleAsync(ruleIdOrAlias, version);
await dr.Management.DeleteRuleByPathAsync(path, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` / `path` | `string` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.LockRuleAsync / LockRuleByPathAsync**

Locks or unlocks a rule.

```csharp
await dr.Management.LockRuleAsync(ruleIdOrAlias, lock, version);
await dr.Management.LockRuleByPathAsync(path, lock, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` / `path` | `string` | no |
| `lock` | `bool` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.FindDuplicatesAsync**

Finds a decision table and returns it with an array of its duplicates.

```csharp
Duplicates result = await dr.Management.FindDuplicatesAsync(ruleIdOrAlias, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.FindDependenciesAsync**

Finds a rule and returns it with an array of its dependencies.

```csharp
Dependencies result = await dr.Management.FindDependenciesAsync(ruleIdOrAlias, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.GetRulesForSpaceAsync**

Gets all rules and ruleflows in the space determined by the Management API Key.

```csharp
Rule[] result = await dr.Management.GetRulesForSpaceAsync();
```

#### **DecisionRules.Management.GetRulesByTagsAsync**

Gets all rules/rule flows with certain tags.

```csharp
Rule[] result = await dr.Management.GetRulesByTagsAsync(tags);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `tags` | `string[]` | no |

#### **DecisionRules.Management.UpdateTagsAsync**

Adds a tag or tags to a specific rule version or all versions of a rule.

```csharp
string[] result = await dr.Management.UpdateTagsAsync(ruleIdOrAlias, tags, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `tags` | `string[]` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.DeleteTagsAsync**

Deletes a tag or tags from a specific rule version or all versions of a rule.

```csharp
await dr.Management.DeleteTagsAsync(ruleIdOrAlias, tags, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `tags` | `string[]` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Management.CreateFolderAsync / CreateFolderByPathAsync**

Creates a new folder under a specified target.

```csharp
await dr.Management.CreateFolderAsync(targetNodeId, data);
await dr.Management.CreateFolderByPathAsync(path, data);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `targetNodeId` / `path` | `string` | no |
| `data` | `FolderData` | no |

**Example:**

```csharp
await dr.Management.CreateFolderAsync("root", 
    new FolderData.Builder()
        .SetType(FolderType.FOLDER)
        .SetName("Folder Name")
        .SetChildren(new List<FolderData>())
        .Build());
```

#### **DecisionRules.Management.UpdateNodeFolderStructureAsync / UpdateNodeFolderStructureByPathAsync**

Creates folders and moves rules into the new structure under a specified target.

```csharp
FolderData result = await dr.Management.UpdateNodeFolderStructureAsync(targetNodeId, data);
FolderData result = await dr.Management.UpdateNodeFolderStructureByPathAsync(path, data);
```

**Arguments:**

| **args** | **type** | **optional** |
|:---|:---|:---|
| `targetNodeId` / `path` | `string` | no |
| `data` | `FolderData` | no |

#### **DecisionRules.Management.ExportFolderAsync / ExportFolderByPathAsync**

Exports a folder with all its rules.

```csharp
FolderExport result = await dr.Management.ExportFolderAsync(nodeId);
FolderExport result = await dr.Management.ExportFolderByPathAsync(path);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `nodeId` / `path` | `string` | no |

#### **DecisionRules.Management.ImportFolderAsync / ImportFolderToPathAsync**

Imports a folder structure into a specific folder.

```csharp
FolderImport result = await dr.Management.ImportFolderAsync(targetNodeId, data);
FolderImport result = await dr.Management.ImportFolderToPathAsync(path, data);
```

**Arguments:**

| **args** | **type** | **optional** |
|:---|:---|:---|
| `targetNodeId` / `path` | `string` | no |
| `data` | `object` | no |

#### **DecisionRules.Management.GetFolderStructureAsync / GetFolderStructureByPathAsync**

Retrieves the folder structure for a given node. If no ID/path is provided, retrieves from the root.

```csharp
FolderData result = await dr.Management.GetFolderStructureAsync(targetNodeId);
FolderData result = await dr.Management.GetFolderStructureByPathAsync(path);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `targetNodeId` / `path` | `string` | yes |

#### **DecisionRules.Management.DeleteFolderAsync / DeleteFolderByPathAsync**

Deletes a folder and all its contents.

```csharp
await dr.Management.DeleteFolderAsync(targetNodeId, deleteAll);
await dr.Management.DeleteFolderByPathAsync(path, deleteAll);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `targetNodeId` / `path` | `string` | no |
| `deleteAll` | `bool` | no |

#### **DecisionRules.Management.RenameFolderAsync / RenameFolderByPathAsync**

Renames a folder.

```csharp
await dr.Management.RenameFolderAsync(targetNodeId, newName);
await dr.Management.RenameFolderByPathAsync(path, newName);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `targetNodeId` / `path` | `string` | no |
| `newName` | `string` | no |

#### **DecisionRules.Management.MoveFolderAsync**

Moves folders and/or rules under a new parent folder.

```csharp
await dr.Management.MoveFolderAsync(targetId, nodes, targetPath);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `targetId` | `string` | no |
| `nodes` | `FolderNode[]` | no |
| `targetPath` | `string` | no |

#### **DecisionRules.Management.FindFolderOrRuleByAttributeAsync**

Finds folders and rules that match the specified criteria. Returns a raw JSON string.

```csharp
string result = await dr.Management.FindFolderOrRuleByAttributeAsync(data);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `data` | `FindOptions` | no |

---

### Job API

All job methods are available under the `dr.Job` object.

#### **DecisionRules.Job.StartAsync**

Starts a new asynchronous job for a specific Integration Flow.

```csharp
Job result = await dr.Job.StartAsync(ruleIdOrAlias, inputData, version);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `ruleIdOrAlias` | `string` | no |
| `inputData` | `object` | no |
| `version` | `int?` | yes |

#### **DecisionRules.Job.CancelAsync**

Attempts to cancel a running job by its ID.

```csharp
Job result = await dr.Job.CancelAsync(jobId);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `jobId` | `string` | no |

#### **DecisionRules.Job.GetInfoAsync**

Retrieves detailed information about a job, including its status and output.

```csharp
Job result = await dr.Job.GetInfoAsync(jobId);
```

**Arguments:**

| **args** | **type** | **optional** |
| :--- | :--- | :--- |
| `jobId` | `string` | no |

---

## Complete Example

```csharp
using DecisionRules;
using DecisionRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Initialize service
        var options = new DecisionRulesOptions(
            "https://api.decisionrules.io",
            "YOUR_SOLVER_KEY",
            "YOUR_MANAGEMENT_KEY"
        );
        var dr = new DecisionRulesService(options);

        // Create a folder
        await dr.Management.CreateFolderByPathAsync("/",
            new FolderData.Builder()
                .SetType(FolderType.FOLDER)
                .SetName("My Folder")
                .SetChildren(new List<FolderData>())
                .Build());

        // Get folder structure
        var folderData = await dr.Management.GetFolderStructureAsync("root");
        var folder = folderData.Children.FirstOrDefault(f => f.Name == "My Folder");

        // Create a rule in the folder
        var rule = new Rule { /* rule properties */ };
        var createdRule = await dr.Management.CreateRuleAsync(rule, "/My Folder");

        // Solve the rule
        var inputData = new Dictionary<string, object>
        {
            ["input"] = new Dictionary<string, object>()
        };
        var result = await dr.SolveAsync(createdRule.RuleId, inputData);

        // Start a job
        var job = await dr.Job.StartAsync(createdRule.RuleId, inputData);
        
        // Get job info
        await Task.Delay(100);
        var jobInfo = await dr.Job.GetInfoAsync(job.JobId);

        // Clean up
        await dr.Management.DeleteFolderByPathAsync("/My Folder", true);
    }
}
```

## License

MIT License

## Support

For issues, questions, or contributions, please visit the [GitHub repository](https://github.com/decisionrules/decisionrules-dotnet).