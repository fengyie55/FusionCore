using FusionConfig.Profiles;
using FusionConfig.Snapshots;
using FusionConfig.Sources;

namespace FusionConfig.Results;

/// <summary>
/// 表示配置加载结果的最小模型。
/// </summary>
/// <param name="ProfileKind">生效 profile 类型。</param>
/// <param name="Sources">参与加载的来源集合。</param>
/// <param name="SectionResults">配置节加载结果集合。</param>
/// <param name="Snapshot">配置快照。</param>
/// <param name="ValidationResult">最小校验结果。</param>
public sealed record ConfigurationLoadResult(
    ConfigurationProfileKind ProfileKind,
    IReadOnlyCollection<ConfigurationSourceDescriptor> Sources,
    IReadOnlyCollection<ConfigurationSectionLoadResult> SectionResults,
    ConfigurationSnapshot Snapshot,
    ConfigurationValidationResult ValidationResult);
