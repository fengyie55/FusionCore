namespace FusionConfig.Runtime;

/// <summary>
/// 表示运行根派生目录集合的最小模型。
/// </summary>
/// <param name="ConfigPath">配置目录。</param>
/// <param name="DataPath">数据目录。</param>
/// <param name="LogsPath">日志目录。</param>
/// <param name="RuntimePath">运行时目录。</param>
/// <param name="TempPath">临时目录。</param>
/// <param name="BackupsPath">备份目录。</param>
/// <param name="DeployPath">部署目录。</param>
/// <param name="ScriptsPath">脚本目录。</param>
public sealed record RuntimePathSet(
    string ConfigPath,
    string DataPath,
    string LogsPath,
    string RuntimePath,
    string TempPath,
    string BackupsPath,
    string DeployPath,
    string ScriptsPath)
{
    /// <summary>
    /// 根据根路径派生目录集合。
    /// </summary>
    /// <param name="rootPath">根路径。</param>
    /// <returns>目录集合。</returns>
    public static RuntimePathSet FromRoot(string rootPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rootPath);

        return new RuntimePathSet(
            Path.Combine(rootPath, "config"),
            Path.Combine(rootPath, "data"),
            Path.Combine(rootPath, "logs"),
            Path.Combine(rootPath, "runtime"),
            Path.Combine(rootPath, "temp"),
            Path.Combine(rootPath, "backups"),
            Path.Combine(rootPath, "deploy"),
            Path.Combine(rootPath, "scripts"));
    }
}
