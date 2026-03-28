namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 导航区的最小入口选项。
/// </summary>
public sealed record StudioNavigationOptions(
    bool IncludeConfigurationEntry,
    bool IncludeLogsEntry,
    bool IncludeRuntimeDiagnosticsEntry,
    bool IncludeDebugAssistantEntry,
    bool IncludeModuleExplorerEntry);
