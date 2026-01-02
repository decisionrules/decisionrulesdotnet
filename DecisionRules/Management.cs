namespace DecisionRules
{
    using DecisionRules.Api;
    // Assuming these namespaces exist in the .NET version of the DecisionRules library
    using DecisionRules.Enums;
    using DecisionRules.Models;
    using DecisionRules.Utilities;
    // Necessary 'using' directives for .NET libraries
    using System;
    using System.Data;
    using System.Threading.Tasks;


    public class Management : IManagement
    {
        private readonly DecisionRulesService _service;
        private readonly ManagementApi managementApi;

        internal Management(DecisionRulesService service, HttpClient httpClient)
        {
            _service = service;
            managementApi = new ManagementApi(httpClient);
        }

        public async Task<Models.Rule> GetRuleAsync(string ruleIdOrAlias, int? version = null)
        {
            try
            {
                return await managementApi.GetRuleApiAsync(_service._options, ruleIdOrAlias, version, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e); // Assuming a static HandleError method
            }
        }

        public async Task<Models.Rule> GetRuleByPathAsync(string path, int? version = null)
        {
            try
            {
                var ruleOptions = new RuleOptions { Path = path, Version = version };
                return await managementApi.GetRuleApiAsync(_service._options, "", version, ruleOptions);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Models.Rule> UpdateRuleStatusAsync(string ruleIdOrAlias, RuleStatus status, int? version)
        {

                return await managementApi.UpdateRuleStatusApiAsync(_service._options, ruleIdOrAlias, status, version);

        }

        public async Task<Models.Rule> UpdateRuleAsync(string ruleIdOrAlias, Models.Rule rule, int? version = null)
        {
            try
            {
                return await managementApi.UpdateRuleApiAsync(_service._options, ruleIdOrAlias, rule, version);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Models.Rule> CreateRuleAsync(Models.Rule rule, string path = null)
        {
            try
            {
                var ruleOptions = !string.IsNullOrEmpty(path) ? new RuleOptions { Path = path } : null;
                return await managementApi.CreateRuleApiAsync(_service._options, rule, ruleOptions);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Models.Rule> CreateNewRuleVersionAsync(string ruleIdOrAlias, Models.Rule rule)
        {
            try
            {
                return await managementApi.CreateNewRuleVersionApiAsync(_service._options, ruleIdOrAlias, rule);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task DeleteRuleAsync(string ruleIdOrAlias, int? version = null)
        {
            try
            {
                await managementApi.DeleteRuleApiAsync(_service._options, ruleIdOrAlias, version, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task DeleteRuleByPathAsync(string path, int? version = null)
        {
            try
            {
                var ruleOptions = new RuleOptions { Path = path, Version = version };
                await managementApi.DeleteRuleApiAsync(_service._options, "", null, ruleOptions);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task LockRuleAsync(string ruleIdOrAlias, bool isLocked, int? version = null)
        {
            try
            {
                await managementApi.LockRuleApiAsync(_service._options, ruleIdOrAlias, isLocked, version, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task LockRuleByPathAsync(string path, bool isLocked, int? version = null)
        {
            try
            {
                var ruleOptions = new RuleOptions { Path = path, Version = version };
                await managementApi.LockRuleApiAsync(_service._options, "", isLocked, version, ruleOptions);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Duplicates> FindDuplicatesAsync(string ruleIdOrAlias, int? version = null)
        {
            try
            {
                return await managementApi.FindDuplicatesApiAsync(_service._options, ruleIdOrAlias, version);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Dependencies> FindDependenciesAsync(string ruleIdOrAlias, int? version = null)
        {
            try
            {
                return await managementApi.FindDependenciesApiAsync(_service._options, ruleIdOrAlias, version);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Models.Rule[]> GetRulesForSpaceAsync()
        {
            try
            {
                return await managementApi.GetRulesForSpaceApiAsync(_service._options);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<Models.Rule[]> GetRulesByTagsAsync(string[] tags)
        {
            try
            {
                return await managementApi.GetRulesByTagsApiAsync(_service._options, tags);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<string[]> UpdateTagsAsync(string ruleIdOrAlias, string[] tags, int? version = null)
        {
            try
            {
                return await managementApi.AddTagsApiAsync(_service._options, ruleIdOrAlias, tags, version);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task DeleteTagsAsync(string ruleIdOrAlias, string[] tags, int? version = null)
        {
            try
            {
                await managementApi.DeleteTagsApiAsync(_service._options, ruleIdOrAlias, tags, version);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task CreateFolderAsync(string targetNodeId, FolderData data)
        {
            try
            {
                await managementApi.CreateFolderApiAsync(_service._options, targetNodeId, data, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task CreateFolderByPathAsync(string path, FolderData data)
        {
            try
            {
                await managementApi.CreateFolderApiAsync(_service._options, "", data, new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderData> UpdateNodeFolderStructureAsync(string targetNodeId, FolderData data)
        {
            try
            {
                return await managementApi.UpdateNodeFolderStructureApiAsync(_service._options, targetNodeId, data, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderData> UpdateNodeFolderStructureByPathAsync(string path, FolderData data)
        {
            try
            {
                return await managementApi.UpdateNodeFolderStructureApiAsync(_service._options, "", data, new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderExport> ExportFolderAsync(string nodeId)
        {
            try
            {
                return await managementApi.ExportFolderApiAsync(_service._options, nodeId, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderExport> ExportFolderByPathAsync(string path)
        {
            try
            {
                return await managementApi.ExportFolderApiAsync(_service._options, "", new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderImport> ImportFolderAsync(string targetNodeId, object data)
        {
            try
            {
                return await managementApi.ImportFolderApiAsync(_service._options, targetNodeId, data, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderImport> ImportFolderToPathAsync(string path, object data)
        {
            try
            {
                return await managementApi.ImportFolderApiAsync(_service._options, "", data, new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderData> GetFolderStructureAsync(string targetNodeId = "")
        {
            try
            {
                return await managementApi.GetFolderStructureApiAsync(_service._options, targetNodeId, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<FolderData> GetFolderStructureByPathAsync(string path)
        {
            try
            {
                return await managementApi.GetFolderStructureApiAsync(_service._options, null, new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task DeleteFolderAsync(string targetNodeId, bool deleteAll)
        {
            try
            {
                await managementApi.DeleteFolderApiAsync(_service._options, targetNodeId, deleteAll, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task DeleteFolderByPathAsync(string path, bool deleteAll)
        {
            try
            {
                await managementApi.DeleteFolderApiAsync(_service._options, "", deleteAll, new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task RenameFolderAsync(string targetNodeId, string newName)
        {
            try
            {
                await managementApi.RenameFolderApiAsync(_service._options, targetNodeId, newName, null);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task RenameFolderByPathAsync(string path, string newName)
        {
            try
            {
                await managementApi.RenameFolderApiAsync(_service._options, "", newName, new FolderOptions { Path = path });
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task MoveFolderAsync(string targetId, FolderNode[] nodes, string targetPath)
        {
            try
            {
                await managementApi.MoveFolderApiAsync(_service._options, targetId, nodes, targetPath);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }

        public async Task<string> FindFolderOrRuleByAttributeAsync(FindOptions data)
        {
            try
            {
                return await managementApi.FindFolderOrRuleByAttributeApiAsync(_service._options, data);
            }
            catch (Exception e)
            {
                throw Utils.HandleError(e);
            }
        }
    }
}
