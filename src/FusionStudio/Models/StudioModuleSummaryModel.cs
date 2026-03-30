namespace FusionStudio.Models;

/// <summary>
/// 表示模块运行摘要的最小只读模型。
/// </summary>
public sealed record StudioModuleSummaryModel(
    string ModuleId,
    string ModuleName,
    string State);
