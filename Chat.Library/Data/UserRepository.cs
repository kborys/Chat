using Chat.Commons.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Versioning;

namespace Chat.Library.Data;

public interface IUserRepository
{
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<bool> CheckExistence(string email);
    Task<int> Create(User user);
    Task AddFriend(int userId, int friendId);
    Task<IEnumerable<User>> GetFriends(int userId);
}

public class UserRepository : IUserRepository
{
    private readonly string _connString;

    public UserRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default");
    }

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task<bool> CheckExistence(string email)
    {
        const string sql = "SELECT CASE WHEN EXISTS (SELECT * FROM [User] WHERE Email = @Email) THEN 1 ELSE 0 END;";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<bool>(sql, new { Email = email });
    }

    public async Task<int> Create(User user)
    {
        const string sql = "DECLARE @InsertedRows AS TABLE (Id int);" +
            "INSERT INTO [User] (Email, FirstName, LastName, Password) " +
            "OUTPUT INSERTED.UserId INTO @InsertedRows " +
            "VALUES (@Email, @FirstName, @LastName, @Password); " +
            "SELECT Id FROM @InsertedRows";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<int>(sql, user);
    }

    public async Task<User?> GetByEmail(string email)
    {
        const string sql = "SELECT * FROM [User] WHERE Email = @Email";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
    }

    public async Task<User?> GetById(int id)
    {
        const string sql = "SELECT * FROM [User] WHERE UserId = @UserId;";

        using var connection = Connection;

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { UserId = id });
    }

    public async Task AddFriend(int userId, int friendId)
    {
        const string sql = "INSERT INTO [dbo].[Friends] (UserId, FriendId) VALUES (@UserId, @FriendId)";

        using var connection = Connection;

        await connection.ExecuteAsync(sql, new { UserId = userId, FriendId = friendId });
    }

    public async Task<IEnumerable<User>> GetFriends(int userId)
    {
        const string sql = "SELECT u.* FROM [dbo].[User] AS u " +
            "INNER JOIN [dbo].[Friends] AS f on u.UserId = f.FriendId " +
            "WHERE f.UserId = @UserId";
        using var connection = Connection;
        
        return await connection.QueryAsync<User>(sql, new { UserId = userId });
    }
}
