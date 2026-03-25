namespace FusionConfig.Runtime;

/// <summary>
/// 表示运行根及其核心目录语义的最小模型。
/// </summary>
/// <param name="LogicalRoot">逻辑运行根。</param>
/// <param name="PhysicalRoot">物理运行根。</param>
/// <param name="PathMode">路径模式。</param>
/// <param name="ConfigPath">配置目录。</param>
/// <param name="DataPath">数据目录。</param>
/// <param name="LogsPath">日志目录。</param>
/// <param name="RuntimePath">运行时目录。</param>
/// <param name="TempPath">临时目录。</param>
/// <param name="BackupsPath">备份目录。</param>
public sealed record RuntimeRootOptions(
    string LogicalRoot,
    string PhysicalRoot,
    RuntimePathMode PathMode,
    string ConfigPath,
    string DataPath,
    string LogsPath,
    string RuntimePath,
    string TempPath,
    string BackupsPath)
{
    /// <summary>
    /// 获取派生目录集合。
    /// </summary>
    public RuntimePathSet PathSet => RuntimePathSet.FromRoot(PhysicalRoot);

    /// <summary>
    /// 创建默认逻辑运行根配置。
    /// </summary>
    /// <param name="logicalRoot">逻辑运行根。</param>
    /// <param name="physicalRoot">物理运行根。</param>
    /// <returns>运行根配置。</returns>
    public static RuntimeRootOptions CreateDefault(string logicalRoot = @"R:\", string? physicalRoot = null)
    {
        var resolvedPhysicalRoot = physicalRoot ?? logicalRoot;
        var pathSet = RuntimePathSet.FromRoot(resolvedPhysicalRoot);

        return new RuntimeRootOptions(
            logicalRoot,
            resolvedPhysicalRoot,
            RuntimePathMode.LogicalRoot,
            pathSet.ConfigPath,
            pathSet.DataPath,
            pathSet.LogsPath,
            pathSet.RuntimePath,
            pathSet.TempPath,
            pathSet.BackupsPath);
    }
}
