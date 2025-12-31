using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.API;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<ITokenService, TokenService>();

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
}