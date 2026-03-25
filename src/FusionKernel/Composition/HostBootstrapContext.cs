namespace FusionKernel.Composition;

/// <summary>
/// 表示宿主接收的最小外部接线边界。
/// </summary>
public sealed record HostBootstrapContext(
    object? ConfigurationProvider = null,
    object? ConfigurationSnapshot = null,
    object? LoggerWriter = null,
    object? LoggerContext = null);
