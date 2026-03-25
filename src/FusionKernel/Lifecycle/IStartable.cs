namespace FusionKernel.Lifecycle;

/// <summary>
/// 定义最小启动能力。
/// </summary>
public interface IStartable
{
    /// <summary>
    /// 启动当前对象。
    /// </summary>
    void Start();
}
