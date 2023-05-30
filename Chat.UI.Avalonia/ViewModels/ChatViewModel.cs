using ReactiveUI;
using System;

namespace Chat.UI.Avalonia.ViewModels;

public class ChatViewModel : ReactiveObject, IRoutableViewModel
{
    // Reference to IScreen that owns the routable view model.
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public ChatViewModel(IScreen screen)
    {
        HostScreen = screen;
    }
}
