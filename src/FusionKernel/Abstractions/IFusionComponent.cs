namespace FusionKernel.Abstractions;

/// <summary>
/// 定义 Fusion 平台基础组件的最小身份语义。
/// </summary>
public interface IFusionComponent
{
    /// <summary>
    /// 获取组件标识。
    /// </summary>
    string Id { get; }

    /// <summary>
    /// 获取组件名称。
    /// </summary>
    string Name { get; }
}
