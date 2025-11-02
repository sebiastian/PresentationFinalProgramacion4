using Application.Abstraction;
using Contract.Location.Request;
using Contract.Location.Response;
using Contract.User.Request;
using Contract.User.Response;

namespace Application.Service;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public bool CreateLocation(CreateLocationRequest request)
    {
        var locationEntity = new Domain.Entity.Location
        {
               Name = request.Name,
               SpaceType = request.SpaceType,
               Description = request.Description
        };
        return _locationRepository.CreateLocation(locationEntity);  

    }

    public bool DeleteLocation(int id)
    {
     return _locationRepository.DeleteLocation(id);
    }

    public List<LocationResponse> GetAllLocation()
    {
        var locations = _locationRepository.GetAllLocation();

        return locations.Select(location => new LocationResponse
        {
               Id = location.Id,
               Name = location.Name,
               SpaceType = location.SpaceType,
               Description = location.Description
        }).ToList();
    }

    public LocationResponse GetLocation(string id)
    {
        var location = _locationRepository.GetLocation(id);
        return new LocationResponse
        {
               Id= location.Id,
               Name = location.Name,
               SpaceType = location.SpaceType,
               Description = location.Description
        };
    }

    public bool UpdateLocation(int id, UpdateLocationRequest request)
    {
        var location = _locationRepository.GetLocation(id.ToString());
        if (location == null)
        {
            return false; // Ubicación no encontrada
        }
        // Actualizar los campos de la ubicación con los datos del request
        location.Name = request.Name;
        location.SpaceType = request.SpaceType;
        location.Description = request.Description;
        return _locationRepository.UpdateLocation(location);
    }


}
