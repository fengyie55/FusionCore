namespace FusionKernel.Runtime;

/// <summary>
/// 表示运行实例标识。
/// </summary>
public sealed record RuntimeInstanceId(string Value)
{
    /// <summary>
    /// 获取实例标识文本。
    /// </summary>
    public override string ToString()
    {
        return Value;
    }
}
