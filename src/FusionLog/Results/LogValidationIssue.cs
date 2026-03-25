namespace FusionLog.Results;

/// <summary>
/// 表示日志校验问题。
/// </summary>
/// <param name="Code">问题代码。</param>
/// <param name="Message">问题消息。</param>
public sealed record LogValidationIssue(
    string Code,
    string Message);
