namespace FusionKernel.Modules;

/// <summary>
/// 定义模块描述信息的最小边界。
/// </summary>
public interface IFusionModuleDescriptor
{
    /// <summary>
    /// 获取模块标识。
    /// </summary>
    string ModuleId { get; }

    /// <summary>
    /// 获取模块名称。
    /// </summary>
    string ModuleName { get; }

    /// <summary>
    /// 获取模块版本。
    /// </summary>
    string Version { get; }
}
