using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonalGestionFinance.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AddressController : ControllerBase
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    /// <summary>
    /// Controller - Get all address
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAddress()
    {
        var response = await _addressService.GetAllAddressService();
        return Ok(response);
    }

    /// <summary>
    /// Controller - get one address
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneAddress(int id)
    {
        var response = _addressService.GetOneAddressService(id);
        return Ok(response);
    }

    /// <summary>
    /// Controller - Add an address
    /// </summary>
    /// <param name="addressRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task AddAddress(AddressRequest addressRequest)
    {
        await _addressService.AddAddress(addressRequest);
    }
    
    /// <summary>
    /// Controller - delete an address
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [HttpDelete("{index}")]
    public async Task DeleteAddress(int index)
    {
        await _addressService.DeleteAddress(index);
    }

}
