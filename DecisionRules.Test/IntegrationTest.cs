using DecisionRules.Enums;
using DecisionRules.Models;
using System.Data;
using System.Text.Json;

namespace DecisionRules.Test
{
    [TestClass]
    public sealed class IntegrationTest
    {
        [TestMethod]
        public async Task ManagementTest()
        {
            Console.WriteLine($"Environment.Version: {Environment.Version}");
            // --- Environment Variable Check ---
            string host = Environment.GetEnvironmentVariable("HOST");
            string solverKey = Environment.GetEnvironmentVariable("SOLVER_KEY");
            string managementKey = Environment.GetEnvironmentVariable("MANAGEMENT_KEY");

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(solverKey) || string.IsNullOrEmpty(managementKey))
            {
                // Assert.Inconclusive is the MSTest equivalent of skipping
                Assert.Inconclusive("Skipping test: Environment variables (HOST, SOLVER_KEY, MANAGEMENT_KEY) are not set.");
                return;
            }

            // Assert.IsNotNull replaces Assert.NotNull
            Assert.IsNotNull(host);
            Assert.IsNotNull(solverKey);
            Assert.IsNotNull(managementKey);

            // Build rule
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };

            var rule = new Models.Rule
            {
                Name = "Integration Flow",
                Description = "",
                Type = "integration-flow",
                Status = "published",
                InputSchema = new Dictionary<string, object> { { "input", new Dictionary<string, object>() } },
                OutputSchema = new Dictionary<string, object> { { "output", new Dictionary<string, object>() } },
                Tags = new List<string>(),
                AuditLog = new Dictionary<string, object>
    {
        { "active", false },
        { "debug", new Dictionary<string, object> { { "active", false } } },
        { "ttl", 14 }
    },
                VisualEditorData = new Dictionary<string, object>
    {
        { "scale", 1 },
        { "rotate", 0 },
        { "translate", new Dictionary<string, object> { { "x", -66 }, { "y", -21 } } }
    },
                RuleAlias = "inland-wolf",
                CreatedIn = new DateTime(2025, 12, 03, 11, 40, 52, 619, DateTimeKind.Utc),
                LastUpdate = new DateTime(2025, 12, 03, 11, 42, 13, 646, DateTimeKind.Utc),
                WorkflowData = new Dictionary<string, object>
    {
        { "nodes", new List<object>
            {
                new Dictionary<string, object> // Node 1 (START)
                {
                    { "connectors", new List<object>
                        {
                            new Dictionary<string, object>
                            {
                                { "type", "out" },
                                { "maxConnections", -1 },
                                { "subType", "none" },
                                { "id", "fbd5a179-588e-4e75-b1e3-73ef458a84a2" },
                                { "maxCount", 1 },
                                { "minCount", 1 }
                            }
                        }
                    },
                    { "id", "c2c8e47c" },
                    { "version", 1 },
                    { "type", "START" },
                    { "position", new Dictionary<string, object> { { "x", 167 }, { "y", 325 } } }
                },
                new Dictionary<string, object> // Node 2 (DATA_MANIPULATION)
                {
                    { "connectors", new List<object>
                        {
                            new Dictionary<string, object>
                            {
                                { "type", "out" },
                                { "maxConnections", -1 },
                                { "subType", "none" },
                                { "id", "29610edc-df5b-4467-a502-5394a66f8f2d" },
                                { "maxCount", 1 },
                                { "minCount", 1 }
                            },
                            new Dictionary<string, object>
                            {
                                { "type", "in" },
                                { "maxConnections", -1 },
                                { "subType", "none" },
                                { "id", "f518b2c4-6579-48d0-adc7-b631216dd610" },
                                { "maxCount", 1 },
                                { "minCount", 1 }
                            }
                        }
                    },
                    { "id", "7e85e5e6" },
                    { "version", 1 },
                    { "type", "DATA_MANIPULATION" },
                    { "position", new Dictionary<string, object> { { "x", 500 }, { "y", 315 } } },
                    { "name", "assign" },
                    { "data", new Dictionary<string, object>
                        {
                            { "mapping", new List<object>
                                {
                                    new Dictionary<string, object>
                                    {
                                        { "source", new Dictionary<string, object>
                                            {
                                                { "expression", new Dictionary<string, object>
                                                    {
                                                        { "type", 1 },
                                                        { "outputScalarValue", new Dictionary<string, object>
                                                            {
                                                                { "type", "function" },
                                                                { "value", "Hello world!" },
                                                                { "stringValue", "Hello world!" }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        { "target", new Dictionary<string, object> { { "path", "output.output" } } }
                                    }
                                }
                            },
                            { "guiSettings", new Dictionary<string, object> { { "showAll", false } } }
                        }
                    }
                },
                new Dictionary<string, object> // Node 3 (HTTP_CLIENT)
                {
                    { "connectors", new List<object>
                        {
                            new Dictionary<string, object>
                            {
                                { "type", "in" },
                                { "maxConnections", -1 },
                                { "subType", "none" },
                                { "id", "caf5ad87-ff5d-4893-89f6-ecd839a2542f" },
                                { "maxCount", 1 },
                                { "minCount", 1 }
                            },
                            new Dictionary<string, object>
                            {
                                { "type", "out" },
                                { "maxConnections", -1 },
                                { "subType", "none" },
                                { "id", "78883421-0045-4353-a901-3ead927470eb" },
                                { "maxCount", 1 },
                                { "minCount", 1 }
                            }
                        }
                    },
                    { "id", "cbfe3058" },
                    { "version", 1 },
                    { "type", "HTTP_CLIENT" },
                    { "position", new Dictionary<string, object> { { "x", 316 }, { "y", 154 } } },
                    { "name", "api" },
                    { "data", new Dictionary<string, object>
                        {
                            { "url", "https://postman-echo.com/delay/3" },
                            { "method", "GET" },
                            { "requestName", "" },
                            { "body", "" },
                            { "bodyFormat", "json" },
                            { "headers", new List<object>
                                {
                                    new Dictionary<string, object> { { "header", "" }, { "value", "" } }
                                }
                            },
                            { "connectionReference", null }
                        }
                    }
                }
            }
        },
        { "connections", new List<object>
            {
                new Dictionary<string, object>
                {
                    { "source", "fbd5a179-588e-4e75-b1e3-73ef458a84a2" },
                    { "target", "caf5ad87-ff5d-4893-89f6-ecd839a2542f" },
                    { "type", 0 },
                    { "id", "1e4ed9eb-122b-4bba-9e42-06b29c2feac9" }
                },
                new Dictionary<string, object>
                {
                    { "source", "78883421-0045-4353-a901-3ead927470eb" },
                    { "target", "f518b2c4-6579-48d0-adc7-b631216dd610" },
                    { "type", 0 },
                    { "id", "057ba96b-c95c-4001-8d14-95153d0026c0" }
                }
            }
        }
    }
            };

            var lookupTableRule = new Models.Rule
            {
                Name = "Testing table 2",
                Description = "",
                Type = "lookup-table",
                Status = "published",
                RuleAlias = "enormous-goldfish",
                CreatedIn = new DateTime(2025, 11, 25, 15, 41, 14, 863, DateTimeKind.Utc),
                LastUpdate = new DateTime(2025, 11, 26, 09, 07, 50, 190, DateTimeKind.Utc),
                Tags = new List<string>(),

                // --- Lookup Table Specific Properties ---
                PrimaryKeyColumn = "pk",

                Columns = new List<object>
    {
        new Dictionary<string, object>
        {
            { "name", "Primary Key" },
            { "alias", "pk" },
            { "order", 0 },
            { "isPrimaryKey", true }
        },
        new Dictionary<string, object>
        {
            { "name", "id" },
            { "alias", "hello" }
        }
    },

                // The 'Data' property (Dictionary/Map format)
                Data = new Dictionary<string, object>
    {
        { "Orange", new Dictionary<string, object> { { "pk", "Orange" }, { "hello", "1" }, { "_position", 0 } } },
        { "Door hinge", new Dictionary<string, object> { { "pk", "Door hinge" }, { "hello", "2" }, { "_position", 1 } } },
        { "Porridge", new Dictionary<string, object> { { "pk", "Porridge" }, { "hello", "3" }, { "_position", 2 } } },
        { "Four inch", new Dictionary<string, object> { { "pk", "Four inch" }, { "hello", "4" }, { "_position", 3 } } },
        { "Forage", new Dictionary<string, object> { { "pk", "Forage" }, { "hello", "5" }, { "_position", 4 } } },
        { "Storage", new Dictionary<string, object> { { "pk", "Storage" }, { "hello", "6" }, { "_position", 5 } } }
    },

                // The 'SourceData' property (Array/List format)
                SourceData = new List<object>
    {
        new Dictionary<string, object> { { "pk", "Orange" }, { "hello", "1" }, { "_position", 0 } },
        new Dictionary<string, object> { { "pk", "Door hinge" }, { "hello", "2" }, { "_position", 1 } },
        new Dictionary<string, object> { { "pk", "Porridge" }, { "hello", "3" }, { "_position", 2 } },
        new Dictionary<string, object> { { "pk", "Four inch" }, { "hello", "4" }, { "_position", 3 } },
        new Dictionary<string, object> { { "pk", "Forage" }, { "hello", "5" }, { "_position", 4 } },
        new Dictionary<string, object> { { "pk", "Storage" }, { "hello", "6" }, { "_position", 5 } }
    },

                // --- Schemas & Meta ---
                InputSchema = new Dictionary<string, object>
    {
        { "primaryKey", new Dictionary<string, object>() },
        { "outputColumn", new Dictionary<string, object>() }
    },
                OutputSchema = new Dictionary<string, object>
    {
        { "output", new Dictionary<string, object>() }
    },
                AuditLog = new Dictionary<string, object>
    {
        { "active", false },
        { "debug", new Dictionary<string, object> { { "active", false } } },
        { "ttl", 14 }
    },
                RuleAliasInfo = new Dictionary<string, object>
    {
        { "usedOn", new List<object>() },
        { "unique", true }
    }
            };

            var dr = new DecisionRulesService(
                        options: new DecisionRulesOptions(host, solverKey, managementKey));

            await dr.Management.CreateFolderAsync("root", new FolderData.Builder().SetType(FolderType.FOLDER)
                .SetName("Folder Name").SetChildren(new List<FolderData>()).Build());

            var folderDataRoot = await dr.Management.GetFolderStructureAsync("root");
            Console.WriteLine("--- GetFolderStructureAsync(root) ---");
            Console.WriteLine(JsonSerializer.Serialize(folderDataRoot));

            var folder = folderDataRoot.Children.FirstOrDefault(f => f.Name == "Folder Name");
            Assert.IsNotNull(folder, "Folder was not created");

            await dr.Management.DeleteFolderAsync(folder.Id, true);

            await dr.Management.CreateFolderByPathAsync("/", new FolderData.Builder().SetType(FolderType.FOLDER)
                .SetName("Folder Name").SetChildren(new List<FolderData>()).Build());

            folderDataRoot = await dr.Management.GetFolderStructureAsync("root");
            Console.WriteLine("--- GetFolderStructureAsync(root 2) ---");
            Console.WriteLine(JsonSerializer.Serialize(folderDataRoot));

            folder = folderDataRoot.Children.FirstOrDefault(f => f.Name == "Folder Name");
            Assert.IsNotNull(folder, "Folder was not created by path");

            Models.Rule createdRule = await dr.Management.CreateRuleAsync(rule, "/Folder Name");
            Console.WriteLine(JsonSerializer.Serialize(lookupTableRule));
            Models.Rule craetedLookupTable = await dr.Management.CreateRuleAsync(lookupTableRule, "/Folder Name");
            string requestBody = "{\"primaryKey\":\"A\",\"outputColumn\":{}}";

            // Perform the rule solving
            await dr.SolveAsync(
                craetedLookupTable.RuleId,
                requestBody,
                1,
              new SolverOptions.Builder().WithLookupMethod(LookupMethodOptions.LOOKUP_EXISTS).Build()
            );

            await dr.SolveAsync(
                craetedLookupTable.RuleId,
                requestBody
            );
            Console.WriteLine("--- CreateRuleAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(createdRule));

            createdRule.Description = "Updated description";

            var ruleByPath = await dr.Management.GetRuleByPathAsync($"/Folder Name/{rule.Name}");
            Console.WriteLine("--- GetRuleByPathAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(ruleByPath));

            var inputData = new Dictionary<string, object> { { "input", new Dictionary<string, object>() } };
            Job job = await dr.Job.StartAsync(createdRule.RuleId, inputData);
            Console.WriteLine("--- Job.StartAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(job));

            await Task.Delay(100); // Replaces Thread.sleep

            var jobInfo = await dr.Job.GetInfoAsync(job.JobId);
            Console.WriteLine("--- Job.GetInfoAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(jobInfo));

            var canceledJob = await dr.Job.CancelAsync(job.JobId);
            Console.WriteLine("--- Job.CancelAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(canceledJob));

            var updatedRule = await dr.Management.UpdateRuleAsync(createdRule.RuleId, createdRule);
            Console.WriteLine("--- UpdateRuleAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(updatedRule));

            var statusRulePending = await dr.Management.UpdateRuleStatusAsync(createdRule.RuleId, RuleStatus.PENDING, 1);
            Console.WriteLine("--- UpdateRuleStatusAsync(PENDING) ---");
            Console.WriteLine(JsonSerializer.Serialize(statusRulePending));

            var statusRulePublished = await dr.Management.UpdateRuleStatusAsync(createdRule.RuleId, RuleStatus.PUBLISHED, 1);
            Console.WriteLine("--- UpdateRuleStatusAsync(PUBLISHED) ---");
            Console.WriteLine(JsonSerializer.Serialize(statusRulePublished));

            await dr.Management.LockRuleAsync(createdRule.RuleId, true);

            await dr.Management.LockRuleAsync(createdRule.RuleId, false);

            await dr.Management.LockRuleByPathAsync($"/Folder Name/{rule.Name}", true, 1);

            await dr.Management.LockRuleByPathAsync($"/Folder Name/{rule.Name}", false, 1);

            var newVersionRule = await dr.Management.CreateNewRuleVersionAsync(createdRule.RuleId, rule);
            Console.WriteLine("--- CreateNewRuleVersionAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(newVersionRule));

            var ruleV1 = await dr.Management.GetRuleAsync(createdRule.RuleId, 1);
            Console.WriteLine("--- GetRuleAsync(v1) ---");
            Console.WriteLine(JsonSerializer.Serialize(ruleV1));

            var ruleV2 = await dr.Management.GetRuleAsync(createdRule.RuleId, 2);
            Console.WriteLine("--- GetRuleAsync(v2) ---");
            Console.WriteLine(JsonSerializer.Serialize(ruleV2));

            var ruleByPathV1 = await dr.Management.GetRuleByPathAsync($"/Folder Name/{rule.Name}", 1);
            Console.WriteLine("--- GetRuleByPathAsync(v1) ---");
            Console.WriteLine(JsonSerializer.Serialize(ruleByPathV1));

            var ruleByPathV2 = await dr.Management.GetRuleByPathAsync($"/Folder Name/{rule.Name}", 2);
            Console.WriteLine("--- GetRuleByPathAsync(v2) ---");
            Console.WriteLine(JsonSerializer.Serialize(ruleByPathV2));

            FolderExport folderExport = await dr.Management.ExportFolderAsync(folder.Id);
            Console.WriteLine("--- ExportFolderAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(folderExport));

            await dr.Management.DeleteRuleAsync(createdRule.RuleId, 1);

            var ruleAfterDeleteV1 = await dr.Management.GetRuleAsync(createdRule.RuleId); // Should get version 2
            Console.WriteLine("--- GetRuleAsync(after v1 delete) ---");
            Console.WriteLine(JsonSerializer.Serialize(ruleAfterDeleteV1));

            await dr.Management.DeleteRuleByPathAsync($"/Folder Name/{rule.Name}", 2);

            var rulesForSpace = await dr.Management.GetRulesForSpaceAsync();
            Console.WriteLine("--- GetRulesForSpaceAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(rulesForSpace));

            await dr.Management.RenameFolderAsync(folder.Id, "New Name");

            var importResult = await dr.Management.ImportFolderAsync("root", folderExport);
            Console.WriteLine("--- ImportFolderAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(importResult));

            folderDataRoot = await dr.Management.GetFolderStructureAsync();
            Console.WriteLine("--- GetFolderStructureAsync(after import) ---");
            Console.WriteLine(JsonSerializer.Serialize(folderDataRoot));

            var folder2 = folderDataRoot.Children.FirstOrDefault(f => f.Name == "Folder Name");
            Assert.IsNotNull(folder2, "Imported folder not found");

            var folder2Structure = await dr.Management.GetFolderStructureAsync(folder2.Id);
            Console.WriteLine("--- GetFolderStructureAsync(folder2) ---");
            Console.WriteLine(JsonSerializer.Serialize(folder2Structure));

            var nodesToMove = new FolderNode[]
            {
            new FolderNode(folder2.Id, FolderType.FOLDER)
            };
            await dr.Management.MoveFolderAsync(folder.Id, nodesToMove, "/New Name");

            var findResult = await dr.Management.FindFolderOrRuleByAttributeAsync(
                new FindOptions(null, null, null, null, null, null, null, FolderType.RULE, null));
            Console.WriteLine("--- FindFolderOrRuleByAttributeAsync ---");
            Console.WriteLine(JsonSerializer.Serialize(findResult));

            await dr.Management.DeleteFolderByPathAsync("/New Name", true);

        }
    }
}
