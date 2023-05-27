using System.ComponentModel.DataAnnotations;

namespace Chat.Commons.Models;

public class AuthRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public AuthRequest(string userName, string password)
    {
        Email = userName;
        Password = password;
    }
}
