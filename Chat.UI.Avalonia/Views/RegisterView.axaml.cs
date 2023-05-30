using Avalonia.ReactiveUI;
using Chat.UI.Avalonia.ViewModels;
using ReactiveUI;

namespace Chat.UI.Avalonia.Views;

public partial class RegisterView : ReactiveUserControl<RegisterViewModel>
{
    public RegisterView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}