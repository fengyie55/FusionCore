using FusionConfig.Abstractions;

namespace FusionConfig.Sources;

/// <summary>
/// 提供最小组合配置来源能力。
/// </summary>
public sealed class CompositeConfigurationSource : IConfigurationSource
{
    /// <summary>
    /// 创建组合配置来源。
    /// </summary>
    /// <param name="sourceId">来源标识。</param>
    /// <param name="sourceName">来源名称。</param>
    /// <param name="sources">内部来源集合。</param>
    public CompositeConfigurationSource(
        string sourceId,
        string sourceName,
        IReadOnlyCollection<IConfigurationSource> sources)
    {
        InnerSources = sources ?? Array.Empty<IConfigurationSource>();
        Descriptor = new ConfigurationSourceDescriptor(sourceId, sourceName, "Composite", null, false);
        Sections = BuildSections(InnerSources);
    }

    /// <summary>
    /// 获取内部来源集合。
    /// </summary>
    public IReadOnlyCollection<IConfigurationSource> InnerSources { get; }

    /// <summary>
    /// 获取来源描述。
    /// </summary>
    public ConfigurationSourceDescriptor Descriptor { get; }

    /// <summary>
    /// 获取组合后的配置节集合。
    /// </summary>
    public IReadOnlyCollection<IConfigurationSection> Sections { get; }

    private static IReadOnlyCollection<IConfigurationSection> BuildSections(IReadOnlyCollection<IConfigurationSource> sources)
    {
        var sections = new List<IConfigurationSection>();
        var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var source in sources)
        {
            foreach (var section in source.Sections)
            {
                if (seenKeys.Add(section.SectionKey.Value))
                {
                    sections.Add(section);
                }
            }
        }

        return sections;
    }
}
