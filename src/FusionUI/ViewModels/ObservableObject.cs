using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FusionUI.ViewModels;

/// <summary>
/// 提供最小属性变更通知能力。
/// </summary>
public abstract class ObservableObject : INotifyPropertyChanged
{
    /// <summary>
    /// 属性变更事件。
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// 设置字段并在值变化时通知界面更新。
    /// </summary>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
