using System.Text;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Services;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"] ?? throw new Exception("Token key not. found - Program.cs")))
                };
            });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMemberRepository, MemberRepository>();

        services.AddCors(options =>
        {
            options.AddPolicy("AngularCors", policy =>
            {
                policy
                    .WithOrigins("https://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string connectionString = Environment.GetEnvironmentVariable("GetConnectionStrings__DefaultConnection")!;
            options.UseSqlServer(connectionString ?? configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

    public static UserDTO toUserDTO(this ApplicationUser user, ITokenService tokenService)
    {
        return new UserDTO
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }
}