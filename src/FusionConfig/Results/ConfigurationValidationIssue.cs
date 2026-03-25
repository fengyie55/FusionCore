namespace FusionConfig.Results;

/// <summary>
/// 表示单个配置校验问题。
/// </summary>
/// <param name="Severity">问题级别。</param>
/// <param name="Code">问题代码。</param>
/// <param name="SectionName">配置节名称。</param>
/// <param name="SourceId">来源标识。</param>
/// <param name="Message">问题消息。</param>
public sealed record ConfigurationValidationIssue(
    ConfigurationValidationSeverity Severity,
    string Code,
    string? SectionName,
    string? SourceId,
    string Message);
