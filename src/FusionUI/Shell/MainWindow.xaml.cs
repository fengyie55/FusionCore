using System.Windows;
using FusionUI.Composition;
using FusionUI.Navigation;

namespace FusionUI.Shell;

/// <summary>
/// 表示 FusionUI 的最小 Shell 窗口。
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// 初始化 Shell 窗口。
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        DataContext = UiCompositionRoot.CreateShell();
    }

    private void OnNavigationItemClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not ShellViewModel shellViewModel)
        {
            return;
        }

        if (sender is FrameworkElement { DataContext: NavigationItem item })
        {
            shellViewModel.NavigateTo(item);
        }
    }
}
