namespace FusionStudio.Models;

/// <summary>
/// 表示工程树节点的最小语义分类。
/// </summary>
public enum StudioEngineeringNodeKind
{
    Device = 0,
    Module = 1,
    Parameters = 2,
    Io = 3,
    Alarms = 4,
    Interlocks = 5,
    State = 6,
    Debug = 7
}
