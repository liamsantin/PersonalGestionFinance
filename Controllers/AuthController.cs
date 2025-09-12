using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPersonalGestionFinance.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{

    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Register of user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        await _authService.RegisterService(request);
        return Ok("Utilisateur enregistré avec succès");
    }

    /// <summary>
    /// Login of user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var token = await _authService.AuthenticateService(request.Email, request.Password);
        if (token == null) return Unauthorized("Email ou mot de passe incorrect");

        return Ok(new {Token = token });
    }
}
