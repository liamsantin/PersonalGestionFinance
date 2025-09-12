using ApiPersonalGestionFinance.Database;
using ApiPersonalGestionFinance.Entities;
using ApiPersonalGestionFinance.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace ApiPersonalGestionFinance.Services;

public class AuthService
{
    private readonly UserRepository _userRepo;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _config;

    public AuthService(UserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _passwordHasher = new PasswordHasher<User>();
        _config = config;
    }

    public async Task<string?> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepo.GetByEmailAsync(email);
        if (user == null) return null;

        // var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        // if (result == PasswordVerificationResult.Failed) return null;

        if (user.Email != email || user.Password != password) return null;

        return GenerateJwtToken(user);
    }

    public async Task RegisterAsync(string email, string password)
    {
        var user = new User { Email = email, Password = password };
        user.Password = _passwordHasher.HashPassword(user, password);
        await _userRepo.AddAsync(user);
    }

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
