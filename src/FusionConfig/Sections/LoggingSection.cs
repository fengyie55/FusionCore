using FusionConfig.Abstractions;

namespace FusionConfig.Sections;

/// <summary>
/// 表示日志配置节的最小骨架。
/// </summary>
/// <param name="Enabled">是否启用日志模块。</param>
/// <param name="LogsPath">日志根目录。</param>
public sealed record LoggingSection(
    bool Enabled,
    string LogsPath) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKeys.Logging;

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
