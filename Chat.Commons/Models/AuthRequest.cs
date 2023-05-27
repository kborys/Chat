using System.ComponentModel.DataAnnotations;

namespace Chat.Commons.Models;

public class AuthRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public AuthRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
