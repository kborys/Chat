using Chat.Commons.Models;
using ReactiveUI;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;

namespace Chat.UI.Avalonia.ViewModels;

public class LoginViewModel : ReactiveObject, IRoutableViewModel
{
    #region IRoutableViewModel properties
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    #endregion
    public string? Email { get; set; }
    public string? Password { get; set; }

    public LoginViewModel(IScreen screen) => HostScreen = screen;

    public async void OnLogin() 
    { 
        if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) return;

        var client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7110/"),
        };
        try
        {
            var response = await client.PostAsJsonAsync("authenticate", new AuthRequest(Email, Password));
            response.EnsureSuccessStatusCode();
            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (authResponse == null) return;
            
            HostScreen.Router.Navigate.Execute(new ChatViewModel(HostScreen, authResponse));
        }
        catch (Exception)
        {

        }
    }

    public void OnRegister() 
    {
        HostScreen.Router.Navigate.Execute(new RegisterViewModel(HostScreen));
    }
}
