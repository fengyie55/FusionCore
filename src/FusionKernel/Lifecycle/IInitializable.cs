namespace FusionKernel.Lifecycle;

/// <summary>
/// 定义最小初始化能力。
/// </summary>
public interface IInitializable
{
    /// <summary>
    /// 执行初始化。
    /// </summary>
    void Initialize();
}
