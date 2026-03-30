namespace FusionStudio.Models;

/// <summary>
/// 表示模块级工程上下文的只读摘要。
/// </summary>
public sealed record StudioModuleContextModel(
    string ModuleId,
    string ModuleName,
    string ModuleType,
    string ModuleState,
    string RuntimeProfile,
    string RuntimeRoot,
    string SourceSummary);
