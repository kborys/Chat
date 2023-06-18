using Chat.Commons.Models;
using Chat.Library.Data;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Chat.Api;

public class ChatHub : Hub
{
    private readonly IMessageRepository _msgRepo;
    private readonly IUserRepository _userRepo;

    public ChatHub(IMessageRepository msgRepo, IUserRepository userRepo)
    {
        _msgRepo = msgRepo;
        _userRepo = userRepo;
    }

    public async Task SendMessage(Message message)
    {
        await _msgRepo.Create(message);
        await Clients.Users(message.Sender.ToString(), message.Receiver.ToString()).SendAsync("ReceiveMessage", message);
    }

    public async Task AddFriend(string friendEmail)
    {
        var userId = Convert.ToInt32(Context?.UserIdentifier);
        var friend = await _userRepo.GetByEmail(friendEmail);
        if (friend == null) return;

        var friends = await _userRepo.GetFriends(userId);
        if(friends.Any(friend => friend.Email == friendEmail)) return;

        await _userRepo.AddFriend(userId, friend.UserId);
        await Clients.User(userId.ToString()).SendAsync("AddFriend", friend);
    }

    public async Task GetFriends()
    {
        var userId = Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return;

        var friends = await _userRepo.GetFriends(Convert.ToInt32(userId));
        await Clients.User(userId).SendAsync("GetFriends", friends);
    }

    public async Task GetMessages(int friendId)
    {
        var userId = Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return;

        var messages = await _msgRepo.GetMessages(Convert.ToInt32(userId), friendId);
        await Clients.User(userId).SendAsync("GetMessages", messages);
    }
}
