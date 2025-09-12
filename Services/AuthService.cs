using ApiPersonalGestionFinance.Database;
using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Models;
using ApiPersonalGestionFinance.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ApiPersonalGestionFinance.Services;

public class AuthService
{
    private readonly AuthRepository _userRepo;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _config;

    /// <summary>
    /// Constructor of class
    /// </summary>
    /// <param name="userRepo"></param>
    /// <param name="config"></param>
    public AuthService(AuthRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _passwordHasher = new PasswordHasher<User>();
        _config = config;
    }

    public async Task<string?> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepo.GetByEmailAsync(email);
            if (user == null) return null;
        var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isValid) return null;
        return GenerateJwtToken(user);
    }

    public async Task RegisterAsync(AuthRequest authRequest)
    {
        await _userRepo.AddAsync(authRequest);
    }

    /// <summary>
    /// Generate a Jwt token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
