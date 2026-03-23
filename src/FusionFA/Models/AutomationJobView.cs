using FusionDomain.ValueObjects;

namespace FusionFA.Models;

/// <summary>
/// 表示面向自动化侧发布的作业视图。
/// </summary>
public sealed record AutomationJobView(
    ControlJobId ControlJobId,
    ProcessJobId ProcessJobId,
    RecipeId RecipeId,
    string ControlStateCode,
    string ProcessStateCode);
