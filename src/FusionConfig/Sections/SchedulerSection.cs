using FusionConfig.Abstractions;

namespace FusionConfig.Sections;

/// <summary>
/// 表示调度模块配置节的最小骨架。
/// </summary>
/// <param name="Enabled">是否启用调度模块。</param>
/// <param name="RuntimePath">调度模块运行目录。</param>
public sealed record SchedulerSection(
    bool Enabled,
    string RuntimePath) : IConfigurationSection
{
    /// <summary>
    /// 获取配置节键。
    /// </summary>
    public ConfigurationSectionKey SectionKey => ConfigurationSectionKeys.Scheduler;

    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
