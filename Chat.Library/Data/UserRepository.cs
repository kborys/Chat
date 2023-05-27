﻿using Chat.Commons.Contracts;
using Chat.Commons.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Chat.Library.Data;

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
        const string sql = "SELECT EXISTS(SELECT * FROM [User] WHERE Email = @Email;";

        using var connection = Connection;

        return await connection.ExecuteScalarAsync<bool>(sql);
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
}
