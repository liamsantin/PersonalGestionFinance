using ApiPersonalGestionFinance.Database;
using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;
using System.Numerics;

namespace ApiPersonalGestionFinance.Repository;

public class AuthRepository
{
    private readonly string _connectionString;

    public AuthRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Get user by email and password in TA_USER with SELECT
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT user_id, user_username, user_password FROM TA_USER WHERE user_username = @username";
        cmd.Parameters.AddWithValue("username", username);

        var reader = cmd.ExecuteReader();
        if(await reader.ReadAsync())
        {
            return new User
            {
                UserId = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2)
            };
        }
        return null;
    }

    /// <summary>
    /// Add user in TA_USER with INSERT INTO
    /// </summary>
    /// <param name="authRequest"></param>
    /// <returns></returns>
    public async Task AddAsync(AuthRequest authRequest)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(authRequest.Password);

        using var conn = new SqliteConnection(_connectionString);
        await conn.OpenAsync(); 

        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO TA_USER (user_nom, user_prenom, user_username, user_email, user_password, user_phone, addr_id) " +
            "VALUES (@nom, @prenom, @username, @email, @password, @phone, @addrId)";
        cmd.Parameters.AddWithValue("@nom", authRequest.Nom);
        cmd.Parameters.AddWithValue("@prenom", authRequest.Prenom);
        cmd.Parameters.AddWithValue("@username", authRequest.Username);
        cmd.Parameters.AddWithValue("@email", authRequest.Email);
        cmd.Parameters.AddWithValue("@password", hashedPassword);
        cmd.Parameters.AddWithValue("@phone", authRequest.Phone);
        cmd.Parameters.AddWithValue("@addrId", authRequest.AddressId);

        await cmd.ExecuteNonQueryAsync();
    }

    // Vérifie email + password en récupérant le hash
    public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
    {
        var user = await GetByUsernameAsync(username);
        if (user == null) return null;

        return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
    }
}
