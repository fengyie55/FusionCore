using FusionConfig.Abstractions;
using FusionConfig.Sections;
using FusionConfig.Sources;

namespace FusionConfig.Snapshots;

/// <summary>
/// 表示单个配置节快照。
/// </summary>
/// <typeparam name="TSection">配置节类型。</typeparam>
/// <param name="SectionKey">配置节键。</param>
/// <param name="Section">配置节实例。</param>
/// <param name="Source">来源描述。</param>
public sealed record ConfigurationSectionSnapshot<TSection>(
    ConfigurationSectionKey SectionKey,
    TSection Section,
    ConfigurationSourceDescriptor? Source)
    where TSection : class, IConfigurationSection;
