using FusionConfig.Abstractions;
using FusionLog.Abstractions;
using FusionKernel.Composition;
using FusionKernel.Modules;

namespace FusionApp.Composition;

/// <summary>
/// 表示 FusionApp 启动时的最小接线上下文。
/// </summary>
public sealed record ApplicationBootstrapContext(
    HostBootstrapContext HostBootstrapContext,
    ApplicationOptions Options,
    ApplicationPresentationOptions PresentationOptions,
    IReadOnlyCollection<IFusionModule> Modules);

/// <summary>
/// 表示 FusionApp 面向 UI 的最小只读启动信息。
/// </summary>
public sealed record ApplicationPresentationOptions(
    string ShellTitle,
    string StartRoute,
    string StartupMessage,
    IReadOnlyList<string> ReadOnlyEntryPoints);

/// <summary>
/// 表示 FusionApp 的最小外部接线边界。
/// </summary>
public sealed record ApplicationBoundary(
    IConfigurationProvider? ConfigurationProvider = null,
    IConfigurationSnapshot? ConfigurationSnapshot = null,
    ILoggerWriter? LoggerWriter = null,
    ILoggerContext? LoggerContext = null);
