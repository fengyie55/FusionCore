using FusionConfig.Sections;

namespace FusionConfig.Results;

/// <summary>
/// 表示单个配置节加载结果的最小模型。
/// </summary>
/// <param name="SectionKey">配置节键。</param>
/// <param name="SourceId">来源标识。</param>
/// <param name="Succeeded">是否成功加载。</param>
/// <param name="Message">结果消息。</param>
public sealed record ConfigurationSectionLoadResult(
    ConfigurationSectionKey SectionKey,
    string SourceId,
    bool Succeeded,
    string? Message)
{
    /// <summary>
    /// 获取配置节名称。
    /// </summary>
    public string SectionName => SectionKey.Value;
}
