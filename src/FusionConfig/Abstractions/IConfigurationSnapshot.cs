using FusionConfig.Runtime;
using FusionConfig.Sections;

namespace FusionConfig.Abstractions;

/// <summary>
/// 定义当前一次配置加载视图的最小快照边界。
/// </summary>
public interface IConfigurationSnapshot
{
    /// <summary>
    /// 获取当前生效 profile。
    /// </summary>
    IConfigurationProfile Profile { get; }

    /// <summary>
    /// 获取当前运行根配置。
    /// </summary>
    RuntimeRootOptions RuntimeRoot { get; }

    /// <summary>
    /// 获取当前快照中的配置节集合。
    /// </summary>
    IReadOnlyCollection<IConfigurationSection> Sections { get; }

    /// <summary>
    /// 判断指定配置节是否存在。
    /// </summary>
    /// <param name="sectionKey">配置节键。</param>
    /// <returns>是否存在。</returns>
    bool ContainsSection(ConfigurationSectionKey sectionKey);

    /// <summary>
    /// 尝试按键获取配置节。
    /// </summary>
    /// <param name="sectionKey">配置节键。</param>
    /// <param name="section">配置节实例。</param>
    /// <returns>是否获取成功。</returns>
    bool TryGetSection(ConfigurationSectionKey sectionKey, out IConfigurationSection? section);

    /// <summary>
    /// 尝试按键和类型获取配置节。
    /// </summary>
    /// <typeparam name="TSection">配置节类型。</typeparam>
    /// <param name="sectionKey">配置节键。</param>
    /// <param name="section">配置节实例。</param>
    /// <returns>是否获取成功。</returns>
    bool TryGetSection<TSection>(ConfigurationSectionKey sectionKey, out TSection? section)
        where TSection : class, IConfigurationSection;
}
