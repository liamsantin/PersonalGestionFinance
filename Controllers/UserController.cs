using ApiPersonalGestionFinance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace ApiPersonalGestionFinance.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _userService.GetAllUsersService();
        return Ok(response);
    }
}
