using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Models.Responses;
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
    public async Task<List<AddressResponse>> GetAllAddress()
    {
        var address = new List<AddressResponse>();

        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT a.addr_id, a.addr_street, a.addr_postalCode, a.addr_city, c.country_name, c.country_iso" +
                          " FROM TA_ADDRESS a INNER JOIN TA_COUNTRY c ON a.country_id = c.country_id";

        var reader = cmd.ExecuteReader();

        while(await reader.ReadAsync())
        {
            address.Add(BuildAddressWithCountry(reader));
        }

        return address;
    }

    public async Task<AddressResponse> GetOneAddress(int id)
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT a.addr_id, a.addr_street, a.addr_postalCode, a.addr_city, c.country_name, c.country_iso" +
                          " FROM TA_ADDRESS a INNER JOIN TA_COUNTRY c ON a.country_id = c.country_id " +
                          " where a.addr_id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        var reader = cmd.ExecuteReader();

        if (await reader.ReadAsync())
        {
            return BuildAddressWithCountry(reader);
        }

        return null;
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

        var sql = @"INSERT INTO TA_ADDRESS (addr_street, addr_postalCode, addr_city, country_id)
                VALUES ($street, $postalCode, $city, $countryId)";

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        command.Parameters.AddWithValue("$street", addressRequest.Street);
        command.Parameters.AddWithValue("$postalCode", addressRequest.PostalCode);
        command.Parameters.AddWithValue("$city", addressRequest.City);
        command.Parameters.AddWithValue("$countryId", addressRequest.CountryId);

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
    private static AddressResponse BuildAddressWithCountry(SqliteDataReader reader)
    {
        return new AddressResponse
        {
            AddressId = reader.GetInt32(0),         // addr_id
            Street = reader.GetString(1),           // street
            PostalCode = reader.GetString(2),             // city
            City = reader.GetString(3),              // zip
            Country = reader.GetString(4),
            ISO = reader.GetString(5)
        };

    }
    #endregion



}


