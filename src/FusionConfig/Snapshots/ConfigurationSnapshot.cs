using FusionConfig.Abstractions;
using FusionConfig.Runtime;
using FusionConfig.Sections;
using FusionConfig.Sources;

namespace FusionConfig.Snapshots;

/// <summary>
/// 提供配置快照的最小默认实现。
/// </summary>
public sealed class ConfigurationSnapshot : IConfigurationSnapshot
{
    private readonly IReadOnlyDictionary<string, IConfigurationSection> _sectionsByKey;
    private readonly IReadOnlyDictionary<string, ConfigurationSourceDescriptor?> _sourcesByKey;

    /// <summary>
    /// 创建配置快照。
    /// </summary>
    /// <param name="profile">当前 profile。</param>
    /// <param name="runtimeRoot">运行根配置。</param>
    /// <param name="sections">配置节集合。</param>
    /// <param name="sectionSources">配置节来源映射。</param>
    public ConfigurationSnapshot(
        IConfigurationProfile profile,
        RuntimeRootOptions runtimeRoot,
        IReadOnlyCollection<IConfigurationSection> sections,
        IReadOnlyDictionary<string, ConfigurationSourceDescriptor?>? sectionSources = null)
    {
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        RuntimeRoot = runtimeRoot ?? throw new ArgumentNullException(nameof(runtimeRoot));
        Sections = sections ?? Array.Empty<IConfigurationSection>();
        _sectionsByKey = Sections.ToDictionary(section => section.SectionKey.Value, StringComparer.OrdinalIgnoreCase);
        _sourcesByKey = sectionSources ?? new Dictionary<string, ConfigurationSourceDescriptor?>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 获取当前生效 profile。
    /// </summary>
    public IConfigurationProfile Profile { get; }

    /// <summary>
    /// 获取当前运行根配置。
    /// </summary>
    public RuntimeRootOptions RuntimeRoot { get; }

    /// <summary>
    /// 获取当前快照中的配置节集合。
    /// </summary>
    public IReadOnlyCollection<IConfigurationSection> Sections { get; }

    /// <summary>
    /// 判断指定配置节是否存在。
    /// </summary>
    /// <param name="sectionKey">配置节键。</param>
    /// <returns>是否存在。</returns>
    public bool ContainsSection(ConfigurationSectionKey sectionKey)
    {
        return _sectionsByKey.ContainsKey(sectionKey.Value);
    }

    /// <summary>
    /// 尝试按键获取配置节。
    /// </summary>
    /// <param name="sectionKey">配置节键。</param>
    /// <param name="section">配置节实例。</param>
    /// <returns>是否获取成功。</returns>
    public bool TryGetSection(ConfigurationSectionKey sectionKey, out IConfigurationSection? section)
    {
        var found = _sectionsByKey.TryGetValue(sectionKey.Value, out var resolvedSection);
        section = resolvedSection;
        return found;
    }

    /// <summary>
    /// 尝试按键和类型获取配置节。
    /// </summary>
    /// <typeparam name="TSection">配置节类型。</typeparam>
    /// <param name="sectionKey">配置节键。</param>
    /// <param name="section">配置节实例。</param>
    /// <returns>是否获取成功。</returns>
    public bool TryGetSection<TSection>(ConfigurationSectionKey sectionKey, out TSection? section)
        where TSection : class, IConfigurationSection
    {
        section = null;

        if (!TryGetSection(sectionKey, out var resolvedSection))
        {
            return false;
        }

        section = resolvedSection as TSection;
        return section is not null;
    }

    /// <summary>
    /// 获取指定配置节的快照。
    /// </summary>
    /// <typeparam name="TSection">配置节类型。</typeparam>
    /// <param name="sectionKey">配置节键。</param>
    /// <returns>配置节快照。</returns>
    public ConfigurationSectionSnapshot<TSection>? GetSectionSnapshot<TSection>(ConfigurationSectionKey sectionKey)
        where TSection : class, IConfigurationSection
    {
        if (!TryGetSection(sectionKey, out TSection? section) || section is null)
        {
            return null;
        }

        _sourcesByKey.TryGetValue(sectionKey.Value, out var source);
        return new ConfigurationSectionSnapshot<TSection>(sectionKey, section, source);
    }
}
