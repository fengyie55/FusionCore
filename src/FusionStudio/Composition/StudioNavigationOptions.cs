namespace FusionStudio.Composition;

/// <summary>
/// 表示 FusionStudio 工程工作台导航区的最小入口选项。
/// </summary>
public sealed record StudioNavigationOptions(
    bool IncludeConfigurationEntry,
    bool IncludeAlarmEntry,
    bool IncludeInterlockEntry,
    bool IncludeModuleWorkbenchEntry,
    bool IncludeIoMonitorEntry,
    bool IncludeRuntimeDiagnosticsEntry,
    bool IncludeLogsEntry,
    bool IncludeControlConsoleEntry,
    bool IncludeDebugAssistantEntry);
