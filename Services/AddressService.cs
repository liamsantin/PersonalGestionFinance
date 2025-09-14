using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Models.Responses;
using ApiPersonalGestionFinance.Repository;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
    public async Task<List<AddressResponse>> GetAllAddressService()
    {
        return await _addressRepo.GetAllAddress();
    }

    /// <summary>
    /// Service - get one address
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public async Task<AddressResponse> GetOneAddressService(int id)
    {
        return await _addressRepo.GetOneAddress(id);
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
