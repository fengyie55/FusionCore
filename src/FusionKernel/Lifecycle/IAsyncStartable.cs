namespace FusionKernel.Lifecycle;

/// <summary>
/// 定义最小异步启动能力。
/// </summary>
public interface IAsyncStartable
{
    /// <summary>
    /// 异步启动当前对象。
    /// </summary>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>异步任务。</returns>
    ValueTask StartAsync(CancellationToken cancellationToken = default);
}
