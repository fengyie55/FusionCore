using FusionConfig.Abstractions;
using FusionConfig.Runtime;
using FusionConfig.Sections;

namespace FusionLog.Options;

/// <summary>
/// 提供日志配置到写入选项的最小映射能力。
/// </summary>
public sealed class LoggingOptionsBinder
{
    /// <summary>
    /// 从日志配置节绑定写入选项。
    /// </summary>
    /// <param name="section">日志配置节。</param>
    /// <returns>日志写入选项。</returns>
    public LoggingWriterOptions Bind(LoggingSection section)
    {
        return Bind(section, RuntimeRootOptions.CreateDefault());
    }

    /// <summary>
    /// 从日志配置节与运行根绑定写入选项。
    /// </summary>
    /// <param name="section">日志配置节。</param>
    /// <param name="runtimeRoot">运行根配置。</param>
    /// <returns>日志写入选项。</returns>
    public LoggingWriterOptions Bind(LoggingSection section, RuntimeRootOptions runtimeRoot)
    {
        ArgumentNullException.ThrowIfNull(section);
        ArgumentNullException.ThrowIfNull(runtimeRoot);

        var resolvedLogsPath = ResolveLogsPath(section.LogsPath, runtimeRoot.PathSet.LogsPath);

        return CreateWriterOptions(
            section.Enabled,
            resolvedLogsPath);
    }

    /// <summary>
    /// 从配置提供者绑定写入选项。
    /// </summary>
    /// <param name="provider">配置提供者。</param>
    /// <returns>日志写入选项。</returns>
    public LoggingWriterOptions Bind(IConfigurationProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);
        var runtimeRoot = provider.GetRuntimeRoot();

        if (provider.TryGetSection<LoggingSection>(ConfigurationSectionKeys.Logging, out var section) &&
            section is not null)
        {
            return Bind(section, runtimeRoot);
        }

        return CreateWriterOptions(false, runtimeRoot.PathSet.LogsPath);
    }

    /// <summary>
    /// 解析日志根目录。
    /// </summary>
    /// <param name="sectionLogsPath">配置节中的日志目录。</param>
    /// <param name="runtimeLogsPath">运行根派生的日志目录。</param>
    /// <returns>解析后的日志目录。</returns>
    private static string ResolveLogsPath(string sectionLogsPath, string runtimeLogsPath)
    {
        if (string.IsNullOrWhiteSpace(sectionLogsPath))
        {
            return runtimeLogsPath;
        }

        var trimmedPath = sectionLogsPath.Trim();
        if (Path.IsPathRooted(trimmedPath))
        {
            return trimmedPath;
        }

        return Path.Combine(runtimeLogsPath, trimmedPath);
    }

    /// <summary>
    /// 创建最小日志写入选项。
    /// </summary>
    /// <param name="enabled">是否启用日志。</param>
    /// <param name="logsPath">日志目录。</param>
    /// <returns>日志写入选项。</returns>
    private static LoggingWriterOptions CreateWriterOptions(bool enabled, string logsPath)
    {
        return new LoggingWriterOptions(
            enabled,
            new FileLoggingOptions(
                enabled,
                new FileLogWriteOptions(
                    logsPath,
                    "fusion.log",
                    IncludeHostDirectory: true,
                    IncludeProcessDirectory: true,
                    IncludeModuleDirectory: false)),
            new MemoryLoggingOptions(false),
            UseNullWriterWhenDisabled: true);
    }
}
