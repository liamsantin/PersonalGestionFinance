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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        await _authService.RegisterAsync(request.Email, request.Password);
        return Ok("Utilisateur enregistré avec succès");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequest request)
    {
        var token = await _authService.AuthenticateAsync(request.Email, request.Password);
        if (token == null) return Unauthorized("Email ou mot de passe incorrect");

        return Ok(new { Token = token });
    }

}
