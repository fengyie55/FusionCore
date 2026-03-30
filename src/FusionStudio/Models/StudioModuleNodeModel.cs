namespace FusionStudio.Models;

/// <summary>
/// 表示工程工作台中的模块树节点摘要。
/// </summary>
public sealed record StudioModuleNodeModel(
    string ModuleId,
    string ModuleName,
    string ModuleType,
    string ModuleState,
    IReadOnlyCollection<string> ToolEntries);
