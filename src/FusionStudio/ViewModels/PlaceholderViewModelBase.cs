namespace FusionStudio.ViewModels;

/// <summary>
/// 表示工作台占位页面的最小视图模型基类。
/// </summary>
public abstract class PlaceholderViewModelBase
{
    /// <summary>
    /// 页面标题。
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// 页面说明。
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// 页面提示。
    /// </summary>
    public string Hint { get; }

    /// <summary>
    /// 初始化占位页面视图模型。
    /// </summary>
    protected PlaceholderViewModelBase(string title, string description, string hint)
    {
        Title = title;
        Description = description;
        Hint = hint;
    }
}
