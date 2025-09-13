using ApiPersonalGestionFinance.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Net;

namespace ApiPersonalGestionFinance.Repository;

public class CountryRepository
{
    private readonly string _connectionString;

    public CountryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Repository - get all countries
    /// </summary>
    /// <returns></returns>
    public async Task<List<Country>> GetAllCountryRepo()
    {
        var countries = new List<Country>();

        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM TA_COUNTRY";

        var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            countries.Add(BuildCountry(reader));
        }

        return countries;
    }

    public async Task<Country> GetOneCountryRepo(int id)
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM TA_COUNTRY where country_id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return BuildCountry(reader);
        }

        return null;
    }

    #region Private methods
    private static Country BuildCountry(SqliteDataReader reader)
    {
        return new Country
        {
            CountryId = reader.GetInt32(0),
            Name = reader.GetString(1),
            ISO = reader.GetString(2),

        };
    }
    #endregion
}
