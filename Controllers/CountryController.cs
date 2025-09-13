using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonalGestionFinance.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CountryController : ControllerBase
{
    private readonly CountryService _countryService;

    public CountryController(CountryService authService)
    {
        _countryService = authService;
    }

    /// <summary>
    /// Controller - get all countries
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCountry()
    {
        var response = await _countryService.GetAllCountryService();
        return Ok(response);
    }

    /// <summary>
    /// Controller - get one country by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneCountry(int id)
    {
        var response = await _countryService.GetOneCountryService(id);
        return Ok(response);
    }
}
