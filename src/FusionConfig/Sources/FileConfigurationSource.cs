using FusionConfig.Abstractions;

namespace FusionConfig.Sources;

/// <summary>
/// 表示文件配置来源的最小模型。
/// </summary>
public sealed class FileConfigurationSource : IConfigurationSource
{
    /// <summary>
    /// 创建文件配置来源。
    /// </summary>
    /// <param name="sourceId">来源标识。</param>
    /// <param name="sourceName">来源名称。</param>
    /// <param name="filePath">文件路径。</param>
    /// <param name="optional">是否可选。</param>
    /// <param name="sections">来源携带的配置节集合。</param>
    public FileConfigurationSource(
        string sourceId,
        string sourceName,
        string filePath,
        bool optional,
        IReadOnlyCollection<IConfigurationSection>? sections = null)
    {
        Descriptor = new ConfigurationSourceDescriptor(sourceId, sourceName, "File", filePath, optional);
        Sections = sections ?? Array.Empty<IConfigurationSection>();
    }

    /// <summary>
    /// 获取来源描述。
    /// </summary>
    public ConfigurationSourceDescriptor Descriptor { get; }

    /// <summary>
    /// 获取来源当前可提供的配置节集合。
    /// </summary>
    public IReadOnlyCollection<IConfigurationSection> Sections { get; }
}
