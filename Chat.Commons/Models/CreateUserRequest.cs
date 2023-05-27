using System.ComponentModel.DataAnnotations;

namespace Chat.Commons.Models;

public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    public CreateUserRequest(string email, string firstName, string lastName, string password)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}
