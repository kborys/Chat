using System.ComponentModel.DataAnnotations;

namespace Chat.Commons.Models;

public class AuthRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    public AuthRequest(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}
