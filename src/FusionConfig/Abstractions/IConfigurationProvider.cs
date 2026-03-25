using FusionConfig.Runtime;
using FusionConfig.Sections;

namespace FusionConfig.Abstractions;

/// <summary>
/// 定义对外提供配置读取能力的最小边界。
/// </summary>
public interface IConfigurationProvider
{
    /// <summary>
    /// 获取当前快照。
    /// </summary>
    IConfigurationSnapshot Current { get; }

    /// <summary>
    /// 获取当前生效 profile。
    /// </summary>
    IConfigurationProfile GetProfile();

    /// <summary>
    /// 获取当前运行根配置。
    /// </summary>
    RuntimeRootOptions GetRuntimeRoot();

    /// <summary>
    /// 判断指定配置节是否存在。
    /// </summary>
    /// <param name="sectionKey">配置节键。</param>
    /// <returns>是否存在。</returns>
    bool ContainsSection(ConfigurationSectionKey sectionKey);

    /// <summary>
    /// 尝试获取指定配置节。
    /// </summary>
    /// <typeparam name="TSection">配置节类型。</typeparam>
    /// <param name="sectionKey">配置节键。</param>
    /// <param name="section">配置节实例。</param>
    /// <returns>是否获取成功。</returns>
    bool TryGetSection<TSection>(ConfigurationSectionKey sectionKey, out TSection? section)
        where TSection : class, IConfigurationSection;
}
