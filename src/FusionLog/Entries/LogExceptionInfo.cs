namespace FusionLog.Entries;

/// <summary>
/// 表示异常摘要信息。
/// </summary>
/// <param name="ExceptionType">异常类型。</param>
/// <param name="Message">异常消息。</param>
public sealed record LogExceptionInfo(
    string ExceptionType,
    string Message);
