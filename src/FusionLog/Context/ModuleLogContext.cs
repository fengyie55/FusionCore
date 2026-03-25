namespace FusionLog.Context;

/// <summary>
/// 表示模块级日志上下文。
/// </summary>
/// <param name="ModuleId">模块标识。</param>
/// <param name="ModuleName">模块名称。</param>
/// <param name="InstanceId">实例标识。</param>
public sealed record ModuleLogContext(
    string? ModuleId,
    string? ModuleName,
    string? InstanceId);
