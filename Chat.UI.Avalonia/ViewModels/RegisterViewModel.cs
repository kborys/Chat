using ReactiveUI;
using System;
using System.Reactive;

namespace Chat.UI.Avalonia.ViewModels;

public class RegisterViewModel : ReactiveObject, IRoutableViewModel
{
    // Reference to IScreen that owns the routable view model.
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public ReactiveCommand<Unit, Unit> BackToLogin => HostScreen.Router.NavigateBack;

    public RegisterViewModel(IScreen screen)
    {
        HostScreen = screen;
    }
}
