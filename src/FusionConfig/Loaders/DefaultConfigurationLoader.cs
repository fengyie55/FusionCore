using FusionConfig.Abstractions;
using FusionConfig.Results;
using FusionConfig.Runtime;
using FusionConfig.Sections;
using FusionConfig.Snapshots;
using FusionConfig.Sources;

namespace FusionConfig.Loaders;

/// <summary>
/// 提供配置加载边界的最小默认实现。
/// </summary>
public sealed class DefaultConfigurationLoader : IConfigurationLoader
{
    /// <summary>
    /// 加载配置来源中的配置节。
    /// </summary>
    /// <param name="sources">配置来源集合。</param>
    /// <param name="profile">当前生效 profile。</param>
    /// <returns>加载结果。</returns>
    public ConfigurationLoadResult Load(
        IReadOnlyCollection<IConfigurationSource> sources,
        IConfigurationProfile profile)
    {
        ArgumentNullException.ThrowIfNull(sources);
        ArgumentNullException.ThrowIfNull(profile);

        var descriptors = sources
            .Select(source => source.Descriptor)
            .ToArray();

        var sectionMap = new Dictionary<string, IConfigurationSection>(StringComparer.OrdinalIgnoreCase);
        var sectionSourceMap = new Dictionary<string, ConfigurationSourceDescriptor?>(StringComparer.OrdinalIgnoreCase);
        var sectionResults = new List<ConfigurationSectionLoadResult>();
        var validationIssues = new List<ConfigurationValidationIssue>();

        foreach (var source in sources)
        {
            foreach (var section in source.Sections)
            {
                if (!sectionMap.ContainsKey(section.SectionKey.Value))
                {
                    sectionMap[section.SectionKey.Value] = section;
                    sectionSourceMap[section.SectionKey.Value] = source.Descriptor;
                }

                sectionResults.Add(new ConfigurationSectionLoadResult(
                    section.SectionKey,
                    source.Descriptor.SourceId,
                    true,
                    null));
            }
        }

        if (string.IsNullOrWhiteSpace(profile.ProfileName))
        {
            validationIssues.Add(new ConfigurationValidationIssue(
                ConfigurationValidationSeverity.Error,
                "PROFILE_NAME_REQUIRED",
                ConfigurationSectionKeys.AppSettings.Value,
                null,
                "Profile 名称不能为空。"));
        }

        var runtimeRoot = ResolveRuntimeRoot(sectionMap, validationIssues);
        var snapshot = new ConfigurationSnapshot(
            profile,
            runtimeRoot,
            sectionMap.Values.ToArray(),
            sectionSourceMap);

        return new ConfigurationLoadResult(
            profile.Kind,
            descriptors,
            sectionResults,
            snapshot,
            new ConfigurationValidationResult(validationIssues));
    }

    private static RuntimeRootOptions ResolveRuntimeRoot(
        IReadOnlyDictionary<string, IConfigurationSection> sectionMap,
        ICollection<ConfigurationValidationIssue> validationIssues)
    {
        if (sectionMap.TryGetValue(ConfigurationSectionKeys.AppSettings.Value, out var appSettingsSection) &&
            appSettingsSection is AppSettingsSection appSettings)
        {
            return appSettings.RuntimeRoot;
        }

        validationIssues.Add(new ConfigurationValidationIssue(
            ConfigurationValidationSeverity.Warning,
            "APP_SETTINGS_MISSING",
            ConfigurationSectionKeys.AppSettings.Value,
            null,
            "未提供 AppSettingsSection，已使用默认逻辑运行根。"));

        return RuntimeRootOptions.CreateDefault();
    }
}
