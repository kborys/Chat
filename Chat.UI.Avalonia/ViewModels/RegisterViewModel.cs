using Chat.Commons.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reactive;

namespace Chat.UI.Avalonia.ViewModels;

public class RegisterViewModel : ReactiveObject, IRoutableViewModel
{
    #region IRoutableViewModel properties
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    #endregion
    public ReactiveCommand<Unit, Unit> BackToLogin => HostScreen.Router.NavigateBack;
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }

    private bool _isRegistrationComplete = false;
    public bool IsRegistrationComplete 
    { 
        get => _isRegistrationComplete; 
        set => this.RaiseAndSetIfChanged(ref _isRegistrationComplete, value);
    }

    private bool _isRegistering = false;
    public bool IsRegistering
    {
        get => _isRegistering;
        set => this.RaiseAndSetIfChanged(ref _isRegistering, value);
    }

    private bool _hasRegistrationFailed = false;
    public bool HasRegistrationFailed
    {
        get => _hasRegistrationFailed;
        set => this.RaiseAndSetIfChanged(ref _hasRegistrationFailed, value);
    }


    public RegisterViewModel(IScreen screen) => HostScreen = screen;

    public async void OnRegister()
    {
        var user = new CreateUserRequest(Email, FirstName, LastName, Password);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(user, new ValidationContext(user), results, true);
        if (!isValid) return;
        
        IsRegistering = true;
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7110/")
        };

        try
        {
            var response = await client.PostAsJsonAsync("register", user);
            response.EnsureSuccessStatusCode();
            IsRegistrationComplete = true;
            IsRegistering = false;
        }
        catch (Exception)
        {
            IsRegistering = false;
        }
    }
}
