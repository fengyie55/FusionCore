namespace FusionLog.Options;

/// <summary>
/// 表示文件写入路径选项。
/// </summary>
/// <param name="RootPath">日志根路径。</param>
/// <param name="FileName">日志文件名。</param>
/// <param name="IncludeHostDirectory">是否按宿主分目录。</param>
/// <param name="IncludeProcessDirectory">是否按进程分目录。</param>
/// <param name="IncludeModuleDirectory">是否按模块分目录。</param>
public sealed record FileLogWriteOptions(
    string RootPath,
    string FileName,
    bool IncludeHostDirectory,
    bool IncludeProcessDirectory,
    bool IncludeModuleDirectory);
