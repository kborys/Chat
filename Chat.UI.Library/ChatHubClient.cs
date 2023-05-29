
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

    public async Task SendMessage(string message, string receiverEmail)
    {
        await ChatConnection.InvokeAsync("SendMessage", message, receiverEmail);
    }
}
