using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.Sqlite;

namespace ApiPersonalGestionFinance.Repository;

public class AddressRepository
{
    private readonly string _connectionString;

    public AddressRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Repository - get all address with select
    /// </summary>
    /// <returns></returns>
    public async Task<List<Address>> GetAllAddress()
    {
        var address = new List<Address>();

        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM TA_ADDRESS";

        var reader = cmd.ExecuteReader();

        while(await reader.ReadAsync())
        {
            address.Add(BuildAddress(reader));
        }

        return address;
    }

    /// <summary>
    /// Repository - add an address with insert into
    /// </summary>
    /// <param name="addressRequest"></param>
    /// <returns></returns>
    public async Task AddAddress(AddressRequest addressRequest)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"INSERT INTO TA_ADDRESS (addr_street, addr_postalCode, addr_city, addr_country)
                VALUES ($street, $postalCode, $city, $country)";

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        command.Parameters.AddWithValue("$street", addressRequest.Street);
        command.Parameters.AddWithValue("$postalCode", addressRequest.PostalCode);
        command.Parameters.AddWithValue("$city", addressRequest.City);
        command.Parameters.AddWithValue("$country", addressRequest.Country);

        await command.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Repository - delete an address with delete
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task DeleteAddress(int index)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"DELETE FROM TA_ADDRESS where addr_id = $index";

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        command.Parameters.AddWithValue("$index", index);

        await command.ExecuteNonQueryAsync();
    }

    /* Private method */
    #region Methode privées
    private static Address BuildAddress(SqliteDataReader reader)
    {
        return new Address
        {
            AddressId = reader.GetInt32(0),         // addr_id
            Street = reader.GetString(1),           // street
            PostalCode = reader.GetString(2),             // city
            City = reader.GetString(3),              // zip
            Country = reader.GetString(4)
        };

    }
    #endregion



}


