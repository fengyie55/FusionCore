using FusionConfig.Abstractions;

namespace FusionConfig.Sources;

/// <summary>
/// 表示内存配置来源的最小模型。
/// </summary>
public sealed class InMemoryConfigurationSource : IConfigurationSource
{
    /// <summary>
    /// 创建内存配置来源。
    /// </summary>
    /// <param name="sourceId">来源标识。</param>
    /// <param name="sourceName">来源名称。</param>
    /// <param name="sections">配置节集合。</param>
    public InMemoryConfigurationSource(
        string sourceId,
        string sourceName,
        IReadOnlyCollection<IConfigurationSection> sections)
    {
        Descriptor = new ConfigurationSourceDescriptor(sourceId, sourceName, "InMemory", null, false);
        Sections = sections ?? Array.Empty<IConfigurationSection>();
    }

    /// <summary>
    /// 获取来源描述。
    /// </summary>
    public ConfigurationSourceDescriptor Descriptor { get; }

    /// <summary>
    /// 获取配置节集合。
    /// </summary>
    public IReadOnlyCollection<IConfigurationSection> Sections { get; }
}
