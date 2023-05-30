using ReactiveUI;
using System.Reactive;

namespace Chat.UI.Avalonia.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    // The Router associated with this Screen.
    // Required by the IScreen interface.
    public RoutingState Router { get; } = new RoutingState();

    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new LoginViewModel(this));
        //// Manage the routing state. Use the Router.Navigate.Execute
        //// command to navigate to different view models. 
        ////
        //// Note, that the Navigate.Execute method accepts an instance 
        //// of a view model, this allows you to pass parameters to 
        //// your view models, or to reuse existing view models.
        ////
        //GoNext = ReactiveCommand.CreateFromObservable(
        //    () => Router.Navigate.Execute(new LoginViewModel(this))
        //);
    }
}