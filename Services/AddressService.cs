using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Repository;
using System.Runtime.CompilerServices;

namespace ApiPersonalGestionFinance.Services;

public class AddressService
{
    private readonly AddressRepository _addressRepo;

    public AddressService(AddressRepository addressRepo)
    {
        _addressRepo = addressRepo;
    }

    /// <summary>
    /// Service - get all address
    /// </summary>
    /// <returns></returns>
    public async Task<List<Address>> GetAllAddressService()
    {
        return await _addressRepo.GetAllAddress();
    }

    /// <summary>
    /// Service - add an address
    /// </summary>
    /// <param name="addressRequest"></param>
    /// <returns></returns>
    public async Task AddAddress(AddressRequest addressRequest)
    {
        await _addressRepo.AddAddress(addressRequest);
    }

    /// <summary>
    /// Service - delete an address
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task DeleteAddress(int index)
    {
        await _addressRepo.DeleteAddress(index);
    }
}
