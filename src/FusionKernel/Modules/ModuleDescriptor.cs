namespace FusionKernel.Modules;

/// <summary>
/// 表示模块描述的最小默认实现。
/// </summary>
public sealed record ModuleDescriptor(
    string ModuleId,
    string ModuleName,
    string Version) : IFusionModuleDescriptor;
