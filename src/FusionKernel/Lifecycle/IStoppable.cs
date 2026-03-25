namespace FusionKernel.Lifecycle;

/// <summary>
/// 定义最小停止能力。
/// </summary>
public interface IStoppable
{
    /// <summary>
    /// 停止当前对象。
    /// </summary>
    void Stop();
}
