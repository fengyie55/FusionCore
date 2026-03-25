using FusionConfig.Abstractions;
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
        ArgumentNullException.ThrowIfNull(section);

        return new LoggingWriterOptions(
            section.Enabled,
            new FileLoggingOptions(
                section.Enabled,
                new FileLogWriteOptions(
                    section.LogsPath,
                    "fusion.log",
                    IncludeHostDirectory: true,
                    IncludeProcessDirectory: true,
                    IncludeModuleDirectory: false)),
            new MemoryLoggingOptions(false),
            UseNullWriterWhenDisabled: true);
    }

    /// <summary>
    /// 从配置提供者绑定写入选项。
    /// </summary>
    /// <param name="provider">配置提供者。</param>
    /// <returns>日志写入选项。</returns>
    public LoggingWriterOptions Bind(IConfigurationProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        if (provider.TryGetSection<LoggingSection>(ConfigurationSectionKeys.Logging, out var section) &&
            section is not null)
        {
            return Bind(section);
        }

        return new LoggingWriterOptions(
            false,
            new FileLoggingOptions(
                false,
                new FileLogWriteOptions(
                    provider.GetRuntimeRoot().LogsPath,
                    "fusion.log",
                    IncludeHostDirectory: true,
                    IncludeProcessDirectory: true,
                    IncludeModuleDirectory: false)),
            new MemoryLoggingOptions(false),
            UseNullWriterWhenDisabled: true);
    }
}
