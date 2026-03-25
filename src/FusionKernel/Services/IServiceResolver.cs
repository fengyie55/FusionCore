namespace FusionKernel.Services;

/// <summary>
/// 定义最小服务解析边界。
/// </summary>
public interface IServiceResolver
{
    /// <summary>
    /// 尝试解析指定服务类型。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <returns>解析结果。</returns>
    object? Resolve(Type serviceType);
}
