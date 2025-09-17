using ApiPersonalGestionFinance.Configurations;
using ApiPersonalGestionFinance.Database;
using ApiPersonalGestionFinance.Repository;
using ApiPersonalGestionFinance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

/**
 * Modélisation de l'API
 * Controller <- Service <- Repository <- Entities/Models and DB Connection
 */

var builder = WebApplication.CreateBuilder(args);

// Db Context --> search in AppSettings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<CountryRepository>();
builder.Services.AddScoped<UserRepository>();

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<UserService>();

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

// Autoriser CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // ton front Vue
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
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

// Utiliser CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization(); // Auth JWT

app.MapControllers();

app.Run();
