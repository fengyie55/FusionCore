namespace FusionLog.Results;

/// <summary>
/// 表示日志写入结果。
/// </summary>
/// <param name="Succeeded">是否写入成功。</param>
/// <param name="ValidationResult">校验结果。</param>
/// <param name="Error">写入错误。</param>
/// <param name="AttemptedWriterCount">尝试写入的 writer 数量。</param>
/// <param name="SuccessfulWriterCount">写入成功的 writer 数量。</param>
public sealed record LogWriteResult(
    bool Succeeded,
    LogValidationResult ValidationResult,
    LogWriteError? Error,
    int AttemptedWriterCount = 1,
    int SuccessfulWriterCount = 0)
{
    /// <summary>
    /// 创建成功写入结果。
    /// </summary>
    /// <param name="validationResult">校验结果。</param>
    /// <returns>写入结果。</returns>
    public static LogWriteResult Success(LogValidationResult validationResult)
    {
        return new LogWriteResult(true, validationResult, null, 1, 1);
    }
}
