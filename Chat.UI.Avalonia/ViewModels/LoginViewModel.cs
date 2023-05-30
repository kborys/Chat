using ReactiveUI;
using System;
using System.Reactive;

namespace Chat.UI.Avalonia.ViewModels;

public class LoginViewModel : ReactiveObject, IRoutableViewModel
{
    // Reference to IScreen that owns the routable view model.
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    // Komenda do przejścia do widoku rejestracji
    public ReactiveCommand<Unit, IRoutableViewModel> ToRegistration { get; }

    public ReactiveCommand<Unit, IRoutableViewModel> ToChat { get; }

    public LoginViewModel(IScreen screen)
    {
        HostScreen = screen;
        ToRegistration = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new RegisterViewModel(HostScreen)));
        ToChat = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new ChatViewModel(HostScreen)));
    }
}
