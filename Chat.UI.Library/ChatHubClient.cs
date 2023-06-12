using Chat.Commons.Models;
using Microsoft.AspNetCore.SignalR.Client;

public class ChatHubClient
{
    public HubConnection ChatConnection;

    public ChatHubClient(string jwt)
    {
        ChatConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7110/chathub", opts =>
            {
                opts.AccessTokenProvider = () => Task.FromResult(jwt);
            })
            .WithAutomaticReconnect()
            .Build();
        ChatConnection.StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public async Task SendMessage(Message msg)
    {
        await ChatConnection.InvokeAsync("SendMessage", msg);
    }

    public async Task AddFriend(int userId, string friendEmail)
    {
        await ChatConnection.InvokeAsync("AddFriend", userId, friendEmail);
    }

    public async Task GetFriends()
    {
        await ChatConnection.InvokeAsync("GetFriends");
    }

    public async Task GetMessages(int friendId)
    {
        await ChatConnection.InvokeAsync("GetMessages", friendId);
    }
}
