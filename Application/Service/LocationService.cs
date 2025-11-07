using Application.Abstraction;
using Application.Interfaces;
using Contract.Location.Request;
using Contract.Location.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Application.Service;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IWeatherService _weatherService;

    public LocationService(ILocationRepository locationRepository, IWeatherService weatherService)
    {
        _locationRepository = locationRepository;
        _weatherService = weatherService;
    }

    public bool CreateLocation(CreateLocationRequest request)
    {
        var locationEntity = new Domain.Entity.Location
        {
            Name = request.Name,
            SpaceType = request.SpaceType,
            Description = request.Description,
            City = request.City
        };
        return _locationRepository.Create(locationEntity);
    }

    public bool DeleteLocation(int id)
    {
        return _locationRepository.Delete(id);
    }

    public async Task<List<LocationResponse>> GetAllLocation()
    {
        var locations = _locationRepository.GetAll();
        var result = new List<LocationResponse>();

        foreach (var location in locations)
        {
            var weatherJson = await _weatherService.GetWeatherAsync(location.City);

            string weatherDescription = "Not available";
            double temperature = 0;
            try
            {
                using var doc = JsonDocument.Parse(weatherJson);
                var root = doc.RootElement;

                // Verificar si es un array y tomar el primer elemento
                JsonElement dataElement = root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0
                    ? root[0]
                    : root;

                if (dataElement.TryGetProperty("current_weather", out var currentWeather))
                {
                    temperature = currentWeather.GetProperty("temperature").GetDouble();
                    var code = currentWeather.GetProperty("weathercode").GetInt32();
                    weatherDescription = code switch
                    {
                        0 => "Clear",
                        1 or 2 or 3 => "Partly cloudy",
                        45 or 48 => "Fog",
                        51 or 53 or 55 or 56 or 57 => "Drizzle",
                        61 or 63 or 65 or 66 or 67 => "Rain",
                        71 or 73 or 75 or 77 or 80 or 81 or 82 => "Thunderstorm",
                        _ => "Unknown"
                    };
                }
            }
            catch
            {
                weatherDescription = "Not available";
            }

            result.Add(new LocationResponse
            {
                Id = location.Id,
                Name = location.Name,
                SpaceType = location.SpaceType,
                Description = location.Description,
                City = location.City,
                WeatherDescription = weatherDescription,
                Temperature = temperature
            });
        }

        return result;
    }

    public LocationResponse GetLocation(string id)
    {
        if (!int.TryParse(id, out var parsedId))
            return null;
        var location = _locationRepository.GetById(parsedId);
        if (location == null) return null;
        return new LocationResponse
        {
            Id = location.Id,
            Name = location.Name,
            SpaceType = location.SpaceType,
            Description = location.Description,
            City = location.City
        };
    }

    public bool UpdateLocation(int id, UpdateLocationRequest request)
    {
        var location = _locationRepository.GetById(id);
        if (location == null)
        {
            return false;
        }
        location.Name = request.Name;
        location.SpaceType = request.SpaceType;
        location.Description = request.Description;
        location.City = request.City;
        return _locationRepository.Update(location);
    }
}
