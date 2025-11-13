using Application.Interfaces;
using Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Servicios de aplicación
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPhotographerService, PhotographerService>();
        services.AddScoped<IPhotoSessionService, PhotoSessionService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IWeatherService, WeatherService>();
        
        return services;
    }
}
