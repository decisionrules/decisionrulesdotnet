using DecisionRules.Enums;
using DecisionRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionRules
{
    public interface IManagement
    {
        Task<Models.Rule> GetRuleAsync(string ruleIdOrAlias, int? version = null);
        Task<Models.Rule> GetRuleByPathAsync(string path, int? version = null);
        Task<Models.Rule> UpdateRuleStatusAsync(string ruleIdOrAlias, RuleStatus status, int? version);
        Task<Models.Rule> UpdateRuleAsync(string ruleIdOrAlias, Models.Rule rule, int? version = null);
        Task<Models.Rule> CreateRuleAsync(Models.Rule rule, string path = null);
        Task<Models.Rule> CreateNewRuleVersionAsync(string ruleIdOrAlias, Models.Rule rule);
        Task DeleteRuleAsync(string ruleIdOrAlias, int? version = null);
        Task DeleteRuleByPathAsync(string path, int? version = null);
        Task LockRuleAsync(string ruleIdOrAlias, bool isLocked, int? version = null);
        Task LockRuleByPathAsync(string path, bool isLocked, int? version = null);
        Task<Duplicates> FindDuplicatesAsync(string ruleIdOrAlias, int? version = null);
        Task<Dependencies> FindDependenciesAsync(string ruleIdOrAlias, int? version = null);
        Task<Models.Rule[]> GetRulesForSpaceAsync();
        Task<Models.Rule[]> GetRulesByTagsAsync(string[] tags);
        Task<string[]> UpdateTagsAsync(string ruleIdOrAlias, string[] tags, int? version = null);
        Task DeleteTagsAsync(string ruleIdOrAlias, string[] tags, int? version = null);
        Task CreateFolderAsync(string targetNodeId, FolderData data);
        Task CreateFolderByPathAsync(string path, FolderData data);
        Task<FolderData> UpdateNodeFolderStructureAsync(string targetNodeId, FolderData data);
        Task<FolderData> UpdateNodeFolderStructureByPathAsync(string path, FolderData data);
        Task<FolderExport> ExportFolderAsync(string nodeId);
        Task<FolderExport> ExportFolderByPathAsync(string path);
        Task<FolderImport> ImportFolderAsync(string targetNodeId, object data);
        Task<FolderImport> ImportFolderToPathAsync(string path, object data);
        Task<FolderData> GetFolderStructureAsync(string targetNodeId = "");
        Task<FolderData> GetFolderStructureByPathAsync(string path);
        Task DeleteFolderAsync(string targetNodeId, bool deleteAll);
        Task DeleteFolderByPathAsync(string path, bool deleteAll);
        Task RenameFolderAsync(string targetNodeId, string newName);
        Task RenameFolderByPathAsync(string path, string newName);
        Task MoveFolderAsync(string targetId, FolderNode[] nodes, string targetPath);
        Task<string> FindFolderOrRuleByAttributeAsync(FindOptions data);

    }
}
