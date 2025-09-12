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

    [HttpGet("test")]
    public IActionResult Get()
    {
        return Ok("sadas");
    }

}
