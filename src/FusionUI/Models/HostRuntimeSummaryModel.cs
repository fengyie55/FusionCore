namespace FusionUI.Models;

/// <summary>
/// 表示宿主运行态的最小只读摘要。
/// </summary>
public sealed record HostRuntimeSummaryModel(
    string HostName,
    string HostState,
    string InitializationState,
    string RuntimeInstanceId,
    string? Profile,
    string RuntimeRoot);
