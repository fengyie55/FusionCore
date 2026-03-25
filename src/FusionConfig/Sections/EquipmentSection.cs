using FusionConfig.Abstractions;

namespace FusionConfig.Sections;

/// <summary>
/// 表示设备相关配置节的最小骨架。
/// </summary>
/// <param name="Enabled">是否启用设备模块。</param>
/// <param name="ConfigPath">设备配置目录。</param>
public sealed record EquipmentSection(
    bool Enabled,
    string ConfigPath) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKeys.Equipment;

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
