using Avalonia.ReactiveUI;
using Chat.UI.Avalonia.ViewModels;
using ReactiveUI;
using System.Reactive;

namespace Chat.UI.Avalonia.Views;

public partial class LoginView : ReactiveUserControl<LoginViewModel>
{
    public LoginView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}