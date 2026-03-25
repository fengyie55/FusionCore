namespace FusionConfig.Results;

/// <summary>
/// 表示配置最小校验结果。
/// </summary>
/// <param name="Issues">校验问题集合。</param>
public sealed record ConfigurationValidationResult(
    IReadOnlyCollection<ConfigurationValidationIssue> Issues)
{
    /// <summary>
    /// 获取是否通过校验。
    /// </summary>
    public bool Succeeded => !HasErrors;

    /// <summary>
    /// 获取是否存在警告。
    /// </summary>
    public bool HasWarnings => Issues.Any(issue => issue.Severity == ConfigurationValidationSeverity.Warning);

    /// <summary>
    /// 获取是否存在错误。
    /// </summary>
    public bool HasErrors => Issues.Any(issue => issue.Severity == ConfigurationValidationSeverity.Error);

    /// <summary>
    /// 创建成功校验结果。
    /// </summary>
    /// <returns>成功结果。</returns>
    public static ConfigurationValidationResult Success()
    {
        return new ConfigurationValidationResult(Array.Empty<ConfigurationValidationIssue>());
    }
}
