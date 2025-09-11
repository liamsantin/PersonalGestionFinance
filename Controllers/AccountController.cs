using ApiPersonalGestionFinance.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonalGestionFinance.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountController : ControllerBase
{

    private readonly AppDbContext _context;

    [HttpGet("test")]
    public IActionResult Get()
    {
        return Ok("sadas");
    }

}
