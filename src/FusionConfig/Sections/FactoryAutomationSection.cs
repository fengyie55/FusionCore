using FusionConfig.Abstractions;

namespace FusionConfig.Sections;

/// <summary>
/// 表示工厂自动化配置节的最小骨架。
/// </summary>
/// <param name="Enabled">是否启用自动化模块。</param>
/// <param name="ConfigPath">自动化配置目录。</param>
public sealed record FactoryAutomationSection(
    bool Enabled,
    string ConfigPath) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKeys.FactoryAutomation;

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
