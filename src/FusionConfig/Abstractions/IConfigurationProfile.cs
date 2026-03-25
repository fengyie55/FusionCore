using FusionConfig.Profiles;

namespace FusionConfig.Abstractions;

/// <summary>
/// 定义配置 profile 的最小语义边界。
/// </summary>
public interface IConfigurationProfile
{
    /// <summary>
    /// 获取 profile 名称。
    /// </summary>
    string ProfileName { get; }

    /// <summary>
    /// 获取 profile 类型。
    /// </summary>
    ConfigurationProfileKind Kind { get; }
}
