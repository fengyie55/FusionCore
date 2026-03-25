using FusionConfig.Abstractions;
using FusionConfig.Profiles;
using FusionConfig.Runtime;

namespace FusionConfig.Sections;

/// <summary>
/// 表示平台级应用配置节的最小骨架。
/// </summary>
/// <param name="RuntimeRoot">运行根配置。</param>
/// <param name="Profile">当前环境 profile。</param>
public sealed record AppSettingsSection(
    RuntimeRootOptions RuntimeRoot,
    EnvironmentProfile Profile) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKeys.AppSettings;

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
