namespace FusionEquipment.Abstractions.Lifecycle;

/// <summary>
/// 定义设备模块共享的最小生命周期能力。
/// </summary>
public interface IModuleLifecycle
{
    /// <summary>
    /// 获取一个值，表示模块是否可参与编排。
    /// </summary>
    bool IsEnabled { get; }
}
