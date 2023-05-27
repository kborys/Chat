using Chat.Commons.Models;

namespace Chat.Commons.Contracts;

public interface IJwtUtils
{
    string GenerateToken(User user);
}