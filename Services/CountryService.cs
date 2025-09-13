using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Repository;

namespace ApiPersonalGestionFinance.Services;

public class CountryService
{
    private readonly CountryRepository _countryRepo;

    public CountryService(CountryRepository countryRepo)
    {
        _countryRepo = countryRepo;
    }

    
    public async Task<List<Country>> GetAllCountryService()
    {
        return await _countryRepo.GetAllCountryRepo();
    }

    public async Task<Country> GetOneCountryService(int id)
    {
        return await _countryRepo.GetOneCountryRepo(id);
    }

}
