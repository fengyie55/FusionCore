using FusionConfig.Abstractions;

namespace FusionConfig.Sections;

/// <summary>
/// 表示界面模块配置节的最小骨架。
/// </summary>
/// <param name="Enabled">是否启用界面模块。</param>
/// <param name="ConfigPath">界面配置目录。</param>
public sealed record UiSection(
    bool Enabled,
    string ConfigPath) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKeys.Ui;

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
