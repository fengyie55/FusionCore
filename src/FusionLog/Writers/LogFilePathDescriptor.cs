namespace FusionLog.Writers;

/// <summary>
/// 表示日志文件路径描述。
/// </summary>
/// <param name="DirectoryPath">目录路径。</param>
/// <param name="FilePath">文件路径。</param>
public sealed record LogFilePathDescriptor(
    string DirectoryPath,
    string FilePath);
