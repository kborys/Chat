using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Chat.UI.Avalonia.ViewModels;
using ReactiveUI;

namespace Chat.UI.Avalonia.Views;

public partial class ChatView : ReactiveUserControl<ChatViewModel>
{
    public ChatView()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}