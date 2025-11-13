using Domain.Abstraction;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using System;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<UserManagerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositorios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPhotographerRepository, PhotographerRepository>();
        services.AddScoped<IPhotoSessionRepository, PhotoSessionRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        // Servicios de infraestructura
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        // HttpClient para OpenMeteo
        services.AddHttpClient("OpenMeteo", client =>
        {
            client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");
        });

        return services;
    }
}
