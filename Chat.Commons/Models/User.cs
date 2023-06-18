using System.Text.Json.Serialization;

namespace Chat.Commons.Models;
public class User
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    [JsonIgnore]
    public string? Password { get; set; }

    private User() //dapper purpose
    {

    }

    public User(string email, string firstName, string lastName, string password, int userId = 0)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        UserId = userId;
    }
}
