using System.Text.Json.Serialization;

namespace Chat.Commons.Models;
public class User
{
    public int UserId { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    private User() //dapper purpose
    {

    }

    public User(int userId, string emailAddress, string firstName, string lastName, string password)
    {
        UserId = userId;
        EmailAddress = emailAddress;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}
