using FusionConfig.Sections;

namespace FusionConfig.Abstractions;

/// <summary>
/// 定义配置节的最小语义边界。
/// </summary>
public interface IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    ConfigurationSectionKey SectionKey { get; }

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    string SectionName { get; }
}
