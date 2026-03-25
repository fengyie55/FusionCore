namespace FusionKernel.Lifecycle;

/// <summary>
/// 定义最小异步停止能力。
/// </summary>
public interface IAsyncStoppable
{
    /// <summary>
    /// 异步停止当前对象。
    /// </summary>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>异步任务。</returns>
    ValueTask StopAsync(CancellationToken cancellationToken = default);
}
