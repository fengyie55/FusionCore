using System.Windows;
using FusionStudio.Composition;
using FusionStudio.Navigation;

namespace FusionStudio.Shell;

/// <summary>
/// 表示 FusionStudio 的最小工作台窗口。
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// 初始化工作台窗口。
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        DataContext = StudioCompositionRoot.CreateShell();
    }

    private void OnNavigationItemClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not StudioShellViewModel shellViewModel)
        {
            return;
        }

        if (sender is FrameworkElement { DataContext: NavigationItem item })
        {
            shellViewModel.NavigateTo(item);
        }
    }
}
