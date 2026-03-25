namespace FusionConfig.Sections;

/// <summary>
/// 提供平台核心配置节键的最小集合。
/// </summary>
public static class ConfigurationSectionKeys
{
    /// <summary>
    /// 应用配置节键。
    /// </summary>
    public static ConfigurationSectionKey AppSettings { get; } = ConfigurationSectionKey.From("AppSettings");

    /// <summary>
    /// 日志配置节键。
    /// </summary>
    public static ConfigurationSectionKey Logging { get; } = ConfigurationSectionKey.From("Logging");

    /// <summary>
    /// 调度配置节键。
    /// </summary>
    public static ConfigurationSectionKey Scheduler { get; } = ConfigurationSectionKey.From("Scheduler");

    /// <summary>
    /// 工厂自动化配置节键。
    /// </summary>
    public static ConfigurationSectionKey FactoryAutomation { get; } = ConfigurationSectionKey.From("FactoryAutomation");

    /// <summary>
    /// 界面配置节键。
    /// </summary>
    public static ConfigurationSectionKey Ui { get; } = ConfigurationSectionKey.From("Ui");

    /// <summary>
    /// 设备配置节键。
    /// </summary>
    public static ConfigurationSectionKey Equipment { get; } = ConfigurationSectionKey.From("Equipment");
}
