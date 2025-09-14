using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models.Responses;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.IO.Pipelines;

namespace ApiPersonalGestionFinance.Repository;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Repository - get all users
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetAllUsersRepo()
    {
        var users = new List<User>();

        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT u.user_id,u.user_nom,u.user_prenom,u.user_email,u.user_password,u.user_iban," +
            "u.user_phone,a.addr_street,a.addr_postalCode,a.addr_city,c.country_name,c.country_iso,u.user_createAt," +
            "u.user_updateAt FROM TA_USER u " +
            "LEFT JOIN TA_ADDRESS a ON u.addr_id = a.addr_id " +
            "LEFT JOIN TA_COUNTRY c ON a.country_id = c.country_id;";

        var reader = cmd.ExecuteReader();

        while (await reader.ReadAsync())
        {
            users.Add(BuildUser(reader));
        }

        return users;
    }

    #region Méthodes privées
    public static User BuildUser(SqliteDataReader reader)
    {
        return new User
        {
            UserId = reader.GetInt32(0),
            Nom = reader.GetString(1),
            Prenom = reader.GetString(2),
            Email = reader.GetString(3),
            Password = reader.GetString(4),
            Iban = reader.GetString(5),
            Phone = reader.GetString(6),
            Street = reader.GetString(7),
            PostalCode = reader.GetString(8),
            City = reader.GetString(9),
            Country = reader.GetString(10),
            ISO = reader.GetString(11),
            CreateAt = reader.GetDateTime(12),
            UpdateAt = reader.GetDateTime(13),
        };
    }

    #endregion

}
