using Avalonia.Collections;
using Chat.Commons.Models;
using DynamicData;
using DynamicData.Kernel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace Chat.UI.Avalonia.ViewModels;

public class ChatViewModel : ReactiveObject, IRoutableViewModel
{
    #region IRoutableViewModel properties
    // Reference to IScreen that owns the routable view model.
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..5];
    #endregion

    private readonly ChatHubClient _chat;
    public User User { get; }
    public AvaloniaList<User> Friends { get; } = new();

    private User? _selectedFriend;
    public User? SelectedFriend
    {
        get => _selectedFriend;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedFriend, value);
            RxApp.MainThreadScheduler.Schedule(LoadMessages);
        }
    }

    private string _messageInput = string.Empty;
    public string MessageInput
    {
        get => _messageInput;
        set => this.RaiseAndSetIfChanged(ref _messageInput, value);
    }
    public AvaloniaList<Message> Messages { get; set; } = new();

    public ChatViewModel(IScreen screen, AuthResponse authResponse)
    {
        HostScreen = screen;
        User = authResponse.User;
        _chat = new ChatHubClient(authResponse.Token);

        _chat.ChatConnection.On<IEnumerable<User>>("GetFriends", Friends.AddRange);
        _chat.ChatConnection.On<Message>("ReceiveMessage", Messages.Add);
        _chat.ChatConnection.On<IEnumerable<Message>>("GetMessages", Messages.AddRange);

        RxApp.MainThreadScheduler.Schedule(LoadFriends);
    }

    private async void LoadFriends() 
    {
        await _chat.GetFriends();
        if (Friends.Count > 0) SelectedFriend = Friends[0];
    }
    private async void LoadMessages() => await _chat.GetMessages(Friends.FirstOrDefault(x => x.Email == SelectedFriend?.Email)!.UserId);
    public async void OnSendButtonClick()
    {
        if (SelectedFriend is null) return;

        var msg = new Message(User.UserId, SelectedFriend.UserId, MessageInput);
        await _chat.SendMessage(msg);
        MessageInput = string.Empty;
    }
}