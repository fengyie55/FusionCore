using FusionDomain.ValueObjects;
using FusionScheduler.Models;

namespace FusionScheduler.Contracts;

/// <summary>
/// 定义调度层所需的只读物料跟踪协作边界。
/// </summary>
public interface IMaterialTracker
{
    /// <summary>
    /// 获取当前面向调度层的物料上下文。
    /// </summary>
    MaterialContext GetMaterialContext(MaterialId materialId);
}
