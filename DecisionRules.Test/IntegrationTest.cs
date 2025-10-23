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
                Name = "Integration Flow", // CHANGED
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
                            VisualEditorData = new Dictionary<string, object> // Maps to 'visualData'
                 {
                     { "scale", 1 },
                     { "rotate", 0 },
                     { "translate", new Dictionary<string, object> { { "x", -67 }, { "y", -1 } } }
                 },
                            // --- ADDED from JSON ---
                            RuleAlias = "convinced-snake",
                            CreatedIn = new DateTime(2025, 10, 23, 7, 31, 18, 650, DateTimeKind.Utc),
                            LastUpdate = new DateTime(2025, 10, 23, 7, 31, 36, 301, DateTimeKind.Utc),
                            // ---
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
                                             { "id", "fbd5a179-588e-4e75-b1e3-73ef458a84a2" }, // ADDED
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
                                             { "id", "894ffb64-e983-4701-bd3d-883ae0dfe4b0" }, // ADDED
                                             { "maxCount", 1 },
                                             { "minCount", 1 }
                                         },
                                         new Dictionary<string, object>
                                         {
                                             { "type", "in" },
                                             { "maxConnections", -1 },
                                             { "subType", "none" },
                                             { "id", "ff995f0a-be34-422d-b1eb-0213dfa38ee4" }, // ADDED
                                             { "maxCount", 1 },
                                             { "minCount", 1 }
                                         }
                                     }
                                 },
                                 { "id", "c78d5ad9" }, // CHANGED
                                 { "version", 1 },
                                 { "type", "DATA_MANIPULATION" },
                                 { "position", new Dictionary<string, object> { { "x", 540 }, { "y", 284 } } }, // CHANGED
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
                                                                             { "value", "Hello!" }, // CHANGED
                                                                             { "stringValue", "Hello!" } // CHANGED
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
                             }
                         }
                     },
                     { "connections", new List<object> // CHANGED (structure and content)
                         {
                             new Dictionary<string, object>
                             {
                                 { "source", "fbd5a179-588e-4e75-b1e3-73ef458a84a2" },
                                 { "target", "ff995f0a-be34-422d-b1eb-0213dfa38ee4" },
                                 { "type", 0 },
                                 { "id", "907c7b80-2529-4e3b-8d19-2ba41e472768" }
                             }
                         }
                     }
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
