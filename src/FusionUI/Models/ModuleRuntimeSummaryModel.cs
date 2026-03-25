namespace FusionUI.Models;

/// <summary>
/// 表示模块运行态的最小只读摘要。
/// </summary>
public sealed record ModuleRuntimeSummaryModel(
    string ModuleId,
    string ModuleName,
    string State);
