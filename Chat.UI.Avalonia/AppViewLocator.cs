using Chat.UI.Avalonia.ViewModels;
using Chat.UI.Avalonia.Views;
using ReactiveUI;
using System;

namespace Chat.UI.Avalonia
{
    public class AppViewLocator : IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string? contract) => viewModel switch
        {
            LoginViewModel context => new LoginView { DataContext = context },
            RegisterViewModel context => new RegisterView { DataContext = context},
            ChatViewModel context => new ChatView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}