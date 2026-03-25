namespace FusionLog.Results;

/// <summary>
/// 表示日志最小校验结果。
/// </summary>
/// <param name="Issues">校验问题集合。</param>
public sealed record LogValidationResult(
    IReadOnlyCollection<LogValidationIssue> Issues)
{
    /// <summary>
    /// 获取是否通过校验。
    /// </summary>
    public bool Succeeded => Issues.Count == 0;

    /// <summary>
    /// 创建成功结果。
    /// </summary>
    /// <returns>成功结果。</returns>
    public static LogValidationResult Success()
    {
        return new LogValidationResult(Array.Empty<LogValidationIssue>());
    }
}
