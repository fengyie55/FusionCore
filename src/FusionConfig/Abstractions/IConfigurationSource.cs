using FusionConfig.Sources;

namespace FusionConfig.Abstractions;

/// <summary>
/// 定义配置来源的最小边界。
/// </summary>
public interface IConfigurationSource
{
    /// <summary>
    /// 获取配置来源描述。
    /// </summary>
    ConfigurationSourceDescriptor Descriptor { get; }

    /// <summary>
    /// 获取来源当前可提供的配置节集合。
    /// </summary>
    IReadOnlyCollection<IConfigurationSection> Sections { get; }
}
