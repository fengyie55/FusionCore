using FusionLog.Writers;

namespace FusionLog.Composition;

/// <summary>
/// 表示日志运行装配描述。
/// </summary>
/// <param name="UsesFileWriter">是否启用文件写入器。</param>
/// <param name="UsesMemoryWriter">是否启用内存写入器。</param>
/// <param name="UsesNullWriter">是否启用空写入器。</param>
/// <param name="FilePath">文件路径描述。</param>
public sealed record LoggingRuntimeDescriptor(
    bool UsesFileWriter,
    bool UsesMemoryWriter,
    bool UsesNullWriter,
    LogFilePathDescriptor? FilePath);
