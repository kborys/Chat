using Avalonia.Collections;
using Avalonia.Media.Imaging;
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
            RxApp.MainThreadScheduler.Schedule(async () =>
            {
                var user = Friends.FirstOrDefault(x => x.Email == SelectedFriend?.Email);
                if (user is null) return;
                Messages.Clear();
                await _chat.GetMessages(user.UserId);
            });
        }
    }

    private string _messageInput = string.Empty;
    public string MessageInput
    {
        get => _messageInput;
        set => this.RaiseAndSetIfChanged(ref _messageInput, value);
    }

    private string _friendInput = string.Empty;
    public string FriendInput
    {
        get => _friendInput;
        set => this.RaiseAndSetIfChanged(ref _friendInput, value);
    }

    public AvaloniaList<Message> Messages { get; set; } = new();

    public ChatViewModel(IScreen screen, AuthResponse authResponse)
    {
        HostScreen = screen;
        User = authResponse.User;
        _chat = new ChatHubClient(authResponse.Token);

        _chat.ChatConnection.On<IEnumerable<User>>("GetFriends", users => {
            Friends.Clear();
            Friends.AddRange(users); 
        });
        _chat.ChatConnection.On<Message>("ReceiveMessage", msg =>
        {
            if (!Friends.Any(x => x.UserId == msg.Sender || x.UserId == msg.Receiver)) return;
            Messages.Add(msg);
        });
        _chat.ChatConnection.On<IEnumerable<Message>>("GetMessages", Messages.AddRange);

        RxApp.MainThreadScheduler.Schedule(async () => await _chat.GetFriends());
    }
    public async void OnSendMessage()
    {
        if (string.IsNullOrEmpty(MessageInput) || SelectedFriend is null) return;

        var msg = new Message(User.UserId, SelectedFriend.UserId, MessageInput);
        await _chat.SendMessage(msg);
        MessageInput = string.Empty;
    }

    public async void OnAddFriend()
    {
        if (string.IsNullOrEmpty(FriendInput)) return;

        await _chat.AddFriend(FriendInput);
        FriendInput = string.Empty;
        await _chat.GetFriends();
    }

    public bool IsSender(Message message)
    {
        //throw new NotImplementedException();
        return message.Sender == User.UserId;
    }
}