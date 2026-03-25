using FusionConfig.Results;

namespace FusionConfig.Abstractions;

/// <summary>
/// 定义配置加载边界的最小契约。
/// </summary>
public interface IConfigurationLoader
{
    /// <summary>
    /// 加载指定来源中的配置节集合。
    /// </summary>
    /// <param name="sources">配置来源集合。</param>
    /// <param name="profile">当前生效 profile。</param>
    /// <returns>加载结果。</returns>
    ConfigurationLoadResult Load(
        IReadOnlyCollection<IConfigurationSource> sources,
        IConfigurationProfile profile);
}
