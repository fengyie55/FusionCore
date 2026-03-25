using FusionLog.Context;
using FusionLog.Options;

namespace FusionLog.Writers;

/// <summary>
/// 提供日志文件路径解析能力。
/// </summary>
public sealed class LogFilePathResolver
{
    /// <summary>
    /// 解析日志文件路径。
    /// </summary>
    /// <param name="options">文件写入选项。</param>
    /// <param name="context">日志上下文。</param>
    /// <returns>文件路径描述。</returns>
    public LogFilePathDescriptor Resolve(FileLogWriteOptions options, LogContext? context)
    {
        ArgumentNullException.ThrowIfNull(options);

        var directoryParts = new List<string> { options.RootPath };

        if (options.IncludeHostDirectory && !string.IsNullOrWhiteSpace(context?.HostId))
        {
            directoryParts.Add(context.HostId);
        }

        if (options.IncludeProcessDirectory && !string.IsNullOrWhiteSpace(context?.ProcessId))
        {
            directoryParts.Add(context.ProcessId);
        }

        if (options.IncludeModuleDirectory && !string.IsNullOrWhiteSpace(context?.ModuleId))
        {
            directoryParts.Add(context.ModuleId);
        }

        var directoryPath = Path.Combine(directoryParts.ToArray());
        var filePath = Path.Combine(directoryPath, options.FileName);
        return new LogFilePathDescriptor(directoryPath, filePath);
    }
}
