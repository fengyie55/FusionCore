using FusionConfig.Abstractions;
using FusionConfig.Sections;

namespace FusionConfig;

/// <summary>
/// 表示通用配置节的最小模型。
/// </summary>
/// <param name="SectionName">配置节名称。</param>
public sealed record ConfigurationSection(string SectionName) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKey.From(SectionName);
}
