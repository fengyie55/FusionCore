namespace FusionStudio.Models;

/// <summary>
/// 表示工具页分发时使用的统一只读上下文。
/// </summary>
public sealed record StudioToolPageContextModel(
    string EquipmentName,
    StudioToolDomain ToolDomain,
    StudioModuleContextModel ModuleContext,
    string SourceSummary);
