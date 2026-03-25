using FusionConfig.Abstractions;
using FusionConfig.Runtime;
using FusionConfig.Sections;

namespace FusionConfig.Providers;

/// <summary>
/// 提供配置读取边界的最小默认实现。
/// </summary>
public sealed class DefaultConfigurationProvider : IConfigurationProvider
{
    /// <summary>
    /// 创建配置提供者。
    /// </summary>
    /// <param name="snapshot">当前配置快照。</param>
    public DefaultConfigurationProvider(IConfigurationSnapshot snapshot)
    {
        Current = snapshot ?? throw new ArgumentNullException(nameof(snapshot));
    }

    /// <summary>
    /// 获取当前快照。
    /// </summary>
    public IConfigurationSnapshot Current { get; }

    /// <summary>
    /// 获取当前生效 profile。
    /// </summary>
    public IConfigurationProfile GetProfile()
    {
        return Current.Profile;
    }

    /// <summary>
    /// 获取当前运行根配置。
    /// </summary>
    public RuntimeRootOptions GetRuntimeRoot()
    {
        return Current.RuntimeRoot;
    }

    /// <summary>
    /// 判断指定配置节是否存在。
    /// </summary>
    /// <param name="sectionKey">配置节键。</param>
    /// <returns>是否存在。</returns>
    public bool ContainsSection(ConfigurationSectionKey sectionKey)
    {
        return Current.ContainsSection(sectionKey);
    }

    /// <summary>
    /// 尝试获取指定配置节。
    /// </summary>
    /// <typeparam name="TSection">配置节类型。</typeparam>
    /// <param name="sectionKey">配置节键。</param>
    /// <param name="section">配置节实例。</param>
    /// <returns>是否获取成功。</returns>
    public bool TryGetSection<TSection>(ConfigurationSectionKey sectionKey, out TSection? section)
        where TSection : class, IConfigurationSection
    {
        return Current.TryGetSection(sectionKey, out section);
    }
}
