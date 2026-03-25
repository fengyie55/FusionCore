using FusionConfig.Abstractions;
using FusionConfig.Profiles;
using FusionConfig.Providers;
using FusionConfig.Runtime;
using FusionConfig.Sections;
using FusionConfig.Snapshots;
using FusionLog.Abstractions;
using FusionLog.Categories;
using FusionLog.Composition;
using FusionLog.Context;
using FusionLog.Entries;
using FusionLog.Levels;
using FusionLog.Options;
using FusionLog.Results;
using FusionLog.Writers;

namespace FusionLog.Tests;

public sealed class LoggingSkeletonTests
{
    [Fact]
    public void Core_Log_Abstractions_And_Entry_Model_Can_Be_Instantiated()
    {
        ILoggerContext context = new LogContext(
            new HostLogContext("HOST-01", "Fusion Host"),
            new ProcessLogContext("PROC-01", "FusionScheduler"),
            new ModuleLogContext("MOD-01", "Scheduler", "INS-01"));
        ILogEntry entry = new LogEntry(
            DateTimeOffset.UtcNow,
            LogLevel.Information,
            ProcessLogCategory.Runtime,
            new LogMessage("启动完成"),
            (LogContext)context,
            new LogEventId(1001, "Started"),
            null,
            new[] { new LogProperty("Profile", "dev") });

        Assert.Equal(LogLevel.Information, entry.Level);
        Assert.Equal(LogCategoryNames.Runtime, entry.Category.Name);
        Assert.Equal("HOST-01", context.HostId);
        Assert.Equal("PROC-01", context.ProcessId);
        Assert.Equal("MOD-01", context.ModuleId);
        Assert.Equal("启动完成", entry.Message.Text);
    }

    [Fact]
    public void Log_Level_And_Category_Can_Express_Minimal_Semantics()
    {
        var category = RuntimeLogCategory.Fault;

        Assert.Equal(LogLevel.Warning - 1, LogLevel.Information);
        Assert.Equal(LogCategoryNames.Fault, category.Name);
        Assert.Equal(LogCategoryNames.Audit, RuntimeLogCategory.Audit.Name);
    }

    [Fact]
    public void Log_Result_Models_Can_Express_Aggregated_Minimal_Semantics()
    {
        var validation = new LogValidationResult(new[] { new LogValidationIssue("WARN-01", "消息为空。") });
        var result = new LogWriteResult(false, validation, new LogWriteError("WRITE_FAILED", "写入失败。", "FileLoggerWriter"), 2, 1);

        Assert.False(result.Succeeded);
        Assert.False(result.ValidationResult.Succeeded);
        Assert.Equal("WRITE_FAILED", result.Error?.Code);
        Assert.Equal(2, result.AttemptedWriterCount);
        Assert.Equal(1, result.SuccessfulWriterCount);
    }

    [Fact]
    public void File_Logger_Writer_Can_Write_Minimal_Log_Entry()
    {
        var tempRoot = CreateTempDirectory();
        try
        {
            var writer = new FileLoggerWriter(
                new FileLogWriteOptions(tempRoot, "fusion.log", true, true, false));
            var entry = CreateEntry("文件写入成功");

            var result = writer.Write(entry);
            var resolver = new LogFilePathResolver();
            var descriptor = resolver.Resolve(new FileLogWriteOptions(tempRoot, "fusion.log", true, true, false), entry.Context);

            Assert.True(result.Succeeded);
            Assert.True(File.Exists(descriptor.FilePath));
            Assert.Contains("文件写入成功", File.ReadAllText(descriptor.FilePath));
        }
        finally
        {
            CleanupTempDirectory(tempRoot);
        }
    }

    [Fact]
    public void File_Path_Resolver_Uses_Context_To_Build_Minimal_Path()
    {
        var resolver = new LogFilePathResolver();
        var descriptor = resolver.Resolve(
            new FileLogWriteOptions(@"D:\FusionLogs", "fusion.log", true, true, true),
            CreateContext());

        Assert.Equal(Path.Combine(@"D:\FusionLogs", "HOST-01", "PROC-01", "MOD-01"), descriptor.DirectoryPath);
        Assert.Equal(Path.Combine(descriptor.DirectoryPath, "fusion.log"), descriptor.FilePath);
    }

    [Fact]
    public void Composite_Logger_Writer_Broadcasts_In_Order_And_Aggregates_Result()
    {
        var tempRoot = CreateTempDirectory();
        try
        {
            var memoryWriter = new MemoryLoggerWriter();
            var fileWriter = new FileLoggerWriter(new FileLogWriteOptions(tempRoot, "fusion.log", false, false, false));
            var composite = new CompositeLoggerWriter(new ILoggerWriter[] { memoryWriter, fileWriter });

            var result = composite.Write(CreateEntry("组合写入"));

            Assert.True(result.Succeeded);
            Assert.Equal(2, result.AttemptedWriterCount);
            Assert.Equal(2, result.SuccessfulWriterCount);
            Assert.Single(memoryWriter.Entries);
        }
        finally
        {
            CleanupTempDirectory(tempRoot);
        }
    }

    [Fact]
    public void Logging_Options_Binder_Can_Map_Logging_Section()
    {
        var section = new LoggingSection(true, @"D:\FusionRuntime\logs");
        var binder = new LoggingOptionsBinder();

        var options = binder.Bind(section);

        Assert.True(options.Enabled);
        Assert.True(options.File.Enabled);
        Assert.Equal(@"D:\FusionRuntime\logs", options.File.WriteOptions.RootPath);
        Assert.False(options.Memory.Enabled);
    }

    [Fact]
    public void Logging_Options_Binder_Can_Map_From_Configuration_Provider()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"R:\", @"D:\FusionRuntime");
        var profile = new EnvironmentProfile("dev", ConfigurationProfileKind.Development, "Development");
        var snapshot = new ConfigurationSnapshot(
            profile,
            runtimeRoot,
            new IConfigurationSection[]
            {
                new AppSettingsSection(runtimeRoot, profile),
                new LoggingSection(true, runtimeRoot.LogsPath),
            });
        var provider = new DefaultConfigurationProvider(snapshot);
        var binder = new LoggingOptionsBinder();

        var options = binder.Bind(provider);

        Assert.True(options.Enabled);
        Assert.Equal(runtimeRoot.LogsPath, options.File.WriteOptions.RootPath);
    }

    [Fact]
    public void Factory_Can_Create_Default_Writer_From_Minimal_Options()
    {
        var tempRoot = CreateTempDirectory();
        try
        {
            var options = new LoggingWriterOptions(
                true,
                new FileLoggingOptions(true, new FileLogWriteOptions(tempRoot, "fusion.log", false, false, false)),
                new MemoryLoggingOptions(true),
                true);
            var factory = new DefaultLoggerWriterFactory();

            var writer = factory.Create(options, CreateContext());
            var descriptor = factory.Describe(options, CreateContext());
            var result = writer.Write(CreateEntry("工厂写入"));

            Assert.True(result.Succeeded);
            Assert.True(descriptor.UsesFileWriter);
            Assert.True(descriptor.UsesMemoryWriter);
        }
        finally
        {
            CleanupTempDirectory(tempRoot);
        }
    }

    [Fact]
    public void Null_Logger_Writer_Remains_Reasonable_Default()
    {
        var writer = new NullLoggerWriter();
        var result = writer.Write(CreateEntry("忽略写入"));

        Assert.True(result.Succeeded);
        Assert.Equal(1, result.SuccessfulWriterCount);
    }

    private static LogEntry CreateEntry(string message)
    {
        return new LogEntry(
            DateTimeOffset.UtcNow,
            LogLevel.Information,
            ProcessLogCategory.Runtime,
            new LogMessage(message),
            CreateContext(),
            null,
            null,
            Array.Empty<LogProperty>());
    }

    private static LogContext CreateContext()
    {
        return new LogContext(
            new HostLogContext("HOST-01", "Fusion Host"),
            new ProcessLogContext("PROC-01", "FusionScheduler"),
            new ModuleLogContext("MOD-01", "Scheduler", "INS-01"));
    }

    private static string CreateTempDirectory()
    {
        var path = Path.Combine(Path.GetTempPath(), "FusionCoreTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return path;
    }

    private static void CleanupTempDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, recursive: true);
        }
    }
}
