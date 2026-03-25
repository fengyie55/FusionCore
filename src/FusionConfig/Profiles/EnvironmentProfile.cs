using FusionConfig.Abstractions;

namespace FusionConfig.Profiles;

/// <summary>
/// 表示运行环境 profile 的最小模型。
/// </summary>
/// <param name="ProfileName">Profile 名称。</param>
/// <param name="Kind">Profile 类型。</param>
/// <param name="EnvironmentName">环境名称。</param>
public sealed record EnvironmentProfile(
    string ProfileName,
    ConfigurationProfileKind Kind,
    string? EnvironmentName) : IConfigurationProfile;
