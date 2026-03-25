namespace FusionConfig.Sections;

/// <summary>
/// 表示配置节身份的最小稳定键。
/// </summary>
public readonly record struct ConfigurationSectionKey
{
    /// <summary>
    /// 创建配置节键。
    /// </summary>
    /// <param name="value">原始键值。</param>
    public ConfigurationSectionKey(string value)
    {
        Value = Normalize(value);
    }

    /// <summary>
    /// 获取标准化后的键值。
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 创建配置节键。
    /// </summary>
    /// <param name="value">原始键值。</param>
    /// <returns>标准化后的配置节键。</returns>
    public static ConfigurationSectionKey From(string value)
    {
        return new ConfigurationSectionKey(value);
    }

    /// <summary>
    /// 返回键值字符串。
    /// </summary>
    /// <returns>键值字符串。</returns>
    public override string ToString()
    {
        return Value;
    }

    private static string Normalize(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        return value.Trim();
    }
}
