namespace FusionConfig.Sources;

/// <summary>
/// 表示配置来源描述的最小模型。
/// </summary>
/// <param name="SourceId">来源标识。</param>
/// <param name="SourceName">来源名称。</param>
/// <param name="SourceType">来源类型。</param>
/// <param name="Location">来源位置。</param>
/// <param name="Optional">是否可选。</param>
public sealed record ConfigurationSourceDescriptor(
    string SourceId,
    string SourceName,
    string SourceType,
    string? Location,
    bool Optional);
