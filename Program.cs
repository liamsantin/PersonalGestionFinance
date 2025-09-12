using ApiPersonalGestionFinance.Configurations;
using ApiPersonalGestionFinance.Database;
using ApiPersonalGestionFinance.Repository;
using ApiPersonalGestionFinance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Db Context --> search in AppSettings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AddressRepository>();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AddressService>();

// JWT config
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,          // pas d’issuer
            ValidateAudience = false,        // pas d’audience
            ValidateLifetime = true,         // vérifie l’expiration
            ValidateIssuerSigningKey = true, // vérifie la clé
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

builder.Services.AddAuthorization(); // Auth JWT
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); // Auth JWT

app.MapControllers();

app.Run();
