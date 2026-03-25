using FusionConfig.Abstractions;
using FusionConfig.Loaders;
using FusionConfig.Profiles;
using FusionConfig.Providers;
using FusionConfig.Results;
using FusionConfig.Runtime;
using FusionConfig.Sections;
using FusionConfig.Snapshots;
using FusionConfig.Sources;

namespace FusionConfig.Tests;

public sealed class ConfigurationSkeletonTests
{
    [Fact]
    public void Core_Configuration_Abstractions_And_Sections_Can_Be_Instantiated()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"R:\", @"D:\FusionRuntime");
        var profile = new EnvironmentProfile("dev", ConfigurationProfileKind.Development, "Development");
        IConfigurationSection appSettings = new AppSettingsSection(runtimeRoot, profile);
        IConfigurationSection logging = new LoggingSection(true, runtimeRoot.LogsPath);
        IConfigurationSection scheduler = new SchedulerSection(true, runtimeRoot.RuntimePath);
        IConfigurationSection automation = new FactoryAutomationSection(false, runtimeRoot.ConfigPath);
        IConfigurationSection ui = new UiSection(true, runtimeRoot.ConfigPath);
        IConfigurationSection equipment = new EquipmentSection(true, runtimeRoot.ConfigPath);

        Assert.Equal(@"R:\", runtimeRoot.LogicalRoot);
        Assert.Equal(@"D:\FusionRuntime", runtimeRoot.PhysicalRoot);
        Assert.Equal(ConfigurationProfileKind.Development, profile.Kind);
        Assert.Equal(ConfigurationSectionKeys.AppSettings, appSettings.SectionKey);
        Assert.Equal(ConfigurationSectionKeys.Logging, logging.SectionKey);
        Assert.Equal(ConfigurationSectionKeys.Scheduler, scheduler.SectionKey);
        Assert.Equal(ConfigurationSectionKeys.FactoryAutomation, automation.SectionKey);
        Assert.Equal(ConfigurationSectionKeys.Ui, ui.SectionKey);
        Assert.Equal(ConfigurationSectionKeys.Equipment, equipment.SectionKey);
    }

    [Fact]
    public void Snapshot_And_Provider_Can_Read_Profile_RuntimeRoot_And_Section()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"R:\", @"D:\FusionRuntime");
        var profile = new EnvironmentProfile("sim", ConfigurationProfileKind.Simulation, "Simulation");
        var sections = new IConfigurationSection[]
        {
            new AppSettingsSection(runtimeRoot, profile),
            new LoggingSection(true, runtimeRoot.LogsPath),
        };
        var snapshot = new ConfigurationSnapshot(profile, runtimeRoot, sections);
        var provider = new DefaultConfigurationProvider(snapshot);

        var foundLogging = provider.TryGetSection<LoggingSection>(ConfigurationSectionKeys.Logging, out var logging);
        var missingScheduler = provider.TryGetSection<SchedulerSection>(ConfigurationSectionKeys.Scheduler, out var scheduler);

        Assert.Equal("sim", provider.GetProfile().ProfileName);
        Assert.Equal(@"D:\FusionRuntime", provider.GetRuntimeRoot().PhysicalRoot);
        Assert.True(provider.ContainsSection(ConfigurationSectionKeys.Logging));
        Assert.True(foundLogging);
        Assert.NotNull(logging);
        Assert.False(missingScheduler);
        Assert.Null(scheduler);
    }

    [Fact]
    public void Composite_Source_Uses_First_Section_In_Order()
    {
        var firstSource = new InMemoryConfigurationSource(
            "MEM-01",
            "内存来源一",
            new IConfigurationSection[] { new LoggingSection(true, @"D:\RuntimeA\logs") });
        var secondSource = new InMemoryConfigurationSource(
            "MEM-02",
            "内存来源二",
            new IConfigurationSection[] { new LoggingSection(false, @"D:\RuntimeB\logs") });
        var composite = new CompositeConfigurationSource(
            "COMP-01",
            "组合来源",
            new IConfigurationSource[] { firstSource, secondSource });

        var section = Assert.IsType<LoggingSection>(composite.Sections.Single());

        Assert.True(section.Enabled);
        Assert.Equal(@"D:\RuntimeA\logs", section.LogsPath);
    }

    [Fact]
    public void Default_Loader_Can_Form_Minimal_Closed_Loop()
    {
        var runtimeRoot = RuntimeRootOptions.CreateDefault(@"R:\", @"D:\FusionRuntime");
        var profile = new EnvironmentProfile("prod", ConfigurationProfileKind.Production, "Production");
        var memorySource = new InMemoryConfigurationSource(
            "MEM-01",
            "内存来源",
            new IConfigurationSection[]
            {
                new AppSettingsSection(runtimeRoot, profile),
                new LoggingSection(true, runtimeRoot.LogsPath),
                new SchedulerSection(true, runtimeRoot.RuntimePath),
            });
        var fileSource = new FileConfigurationSource(
            "FILE-01",
            "文件来源",
            Path.Combine(runtimeRoot.ConfigPath, "appsettings.json"),
            true);
        var loader = new DefaultConfigurationLoader();

        var result = loader.Load(new IConfigurationSource[] { memorySource, fileSource }, profile);
        var provider = new DefaultConfigurationProvider(result.Snapshot);

        Assert.Equal(ConfigurationProfileKind.Production, result.ProfileKind);
        Assert.Equal(2, result.Sources.Count);
        Assert.Equal(3, result.SectionResults.Count);
        Assert.True(result.ValidationResult.Succeeded);
        Assert.True(provider.ContainsSection(ConfigurationSectionKeys.AppSettings));
        Assert.True(provider.TryGetSection<AppSettingsSection>(ConfigurationSectionKeys.AppSettings, out var appSettings));
        Assert.NotNull(appSettings);
        Assert.Equal(@"D:\FusionRuntime", provider.GetRuntimeRoot().PhysicalRoot);
    }

    [Fact]
    public void Validation_Result_Can_Express_Warning_And_Error_Aggregation()
    {
        var validationResult = new ConfigurationValidationResult(
            new[]
            {
                new ConfigurationValidationIssue(
                    ConfigurationValidationSeverity.Warning,
                    "WARN-01",
                    ConfigurationSectionKeys.Logging.Value,
                    "FILE-01",
                    "日志路径使用了默认值。"),
                new ConfigurationValidationIssue(
                    ConfigurationValidationSeverity.Error,
                    "ERR-01",
                    ConfigurationSectionKeys.AppSettings.Value,
                    "FILE-01",
                    "运行根缺失。"),
            });
        var loadResult = new ConfigurationLoadResult(
            ConfigurationProfileKind.Production,
            new[] { new ConfigurationSourceDescriptor("FILE-01", "文件来源", "File", @"R:\config\appsettings.json", false) },
            new[] { new ConfigurationSectionLoadResult(ConfigurationSectionKeys.Logging, "FILE-01", true, null) },
            new ConfigurationSnapshot(
                new EnvironmentProfile("prod", ConfigurationProfileKind.Production, "Production"),
                RuntimeRootOptions.CreateDefault(),
                Array.Empty<IConfigurationSection>()),
            validationResult);

        Assert.False(loadResult.ValidationResult.Succeeded);
        Assert.True(loadResult.ValidationResult.HasWarnings);
        Assert.True(loadResult.ValidationResult.HasErrors);
        Assert.Equal("Logging", loadResult.SectionResults.Single().SectionName);
        Assert.Equal("FILE-01", loadResult.Sources.Single().SourceId);
    }

    [Fact]
    public void Runtime_Path_Set_Can_Derive_Core_Runtime_Directories()
    {
        var pathSet = RuntimePathSet.FromRoot(@"D:\FusionRuntime");

        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "config"), pathSet.ConfigPath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "data"), pathSet.DataPath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "logs"), pathSet.LogsPath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "runtime"), pathSet.RuntimePath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "temp"), pathSet.TempPath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "backups"), pathSet.BackupsPath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "deploy"), pathSet.DeployPath);
        Assert.Equal(Path.Combine(@"D:\FusionRuntime", "scripts"), pathSet.ScriptsPath);
    }
}
