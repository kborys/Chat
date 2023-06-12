using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Chat.Commons.Models;
using Dapper;

namespace Chat.Library.Data;

public interface IMessageRepository
{
    Task Create(Message message);
    Task<IEnumerable<Message>> GetMessages(int senderId, int receiverId);
}

public class MessageRepository : IMessageRepository
{
    private readonly string _connString;

    public MessageRepository(IConfiguration config)
    {
        _connString = config.GetConnectionString("Default");
    }

    private IDbConnection Connection => new SqlConnection(_connString);

    public async Task Create(Message message)
    {
        const string sql = "INSERT INTO [dbo].[Message] (Receiver, Sender, Content) VALUES (@Receiver, @Sender, @Content)";

        using var connection = Connection;

        await connection.ExecuteAsync(sql, message);
    }

    public async Task<IEnumerable<Message>> GetMessages(int senderId, int receiverId)
    {
        const string sql = "SELECT * FROM [dbo].[Message] " +
            "WHERE (Sender = @Sender AND Receiver = @Receiver) OR (Sender = @Receiver AND Receiver = @Sender)";

        using var connection = Connection;

        return await connection.QueryAsync<Message>(sql, new { Sender = senderId, Receiver = receiverId});
    }
}
