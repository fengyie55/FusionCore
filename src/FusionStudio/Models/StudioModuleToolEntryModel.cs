namespace FusionStudio.Models;

/// <summary>
/// 表示模块树下的工程工具入口摘要。
/// </summary>
public sealed record StudioModuleToolEntryModel(
    string ToolKey,
    string Title,
    string Summary);
