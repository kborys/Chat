using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Chat.UI.Avalonia.ViewModels;
using ReactiveUI;

namespace Chat.UI.Avalonia.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        this.AttachDevTools();
        AvaloniaXamlLoader.Load(this);
    }
}