using Chat.Library.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chat.Api;

public class ChatHub : Hub
{
    public async Task SendMessage(string msg, string receiverEmail)
    {
        var identity = Context?.User?.Identity as ClaimsIdentity;
        var email = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

        await Clients.User(receiverEmail).SendAsync("ReceiveMessage", $"{email?.Value}: {msg}");
    }
}
