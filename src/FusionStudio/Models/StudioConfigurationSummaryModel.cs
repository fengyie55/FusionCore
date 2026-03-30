namespace FusionStudio.Models;

/// <summary>
/// 表示工程配置入口的最小只读摘要。
/// </summary>
public sealed record StudioConfigurationSummaryModel(
    bool IsConfigurationAvailable,
    string ConfigRoot,
    string Summary)
{
    /// <summary>
    /// 获取空配置摘要。
    /// </summary>
    public static StudioConfigurationSummaryModel Empty { get; } =
        new(false, "未接入", "当前尚未接入工程配置映射。");
}
