using Application.Abstraction;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Persistence.Repository;

public class LocationRepository : ILocationRepository
{
    // Simulación de base de datos en m emoria
    private static readonly List<Location> _locations = new List<Location>
    {
        new Location
        {
            Id = 1,
            Name = "Parque Independencia",
            SpaceType = "Exterior",
            Description = "Espacio amplio con árboles y buena luz natural"
        },
        new Location
        {
            Id = 2,
            Name = "Estudio Central",
            SpaceType = "Interior",
            Description = "Estudio profesional con fondo blanco y luces LED"
        },
        new Location
        {
            Id = 3,
            Name = "Casa de campo",
            SpaceType = "Exterior",
            Description = "Ambiente rústico con jardín y pileta"
        }
    };

    public bool DeleteLocation(int id)
    {
        var location = _locations.FirstOrDefault(loc => loc.Id == id);
        if (location != null)
        {
            _locations.Remove(location);
            return true;
        }
        return false;
    }

    public List<Location> GetAllLocation()
    {
        // Devolver la lista en memoria
        return _locations;
    }

    public Location GetLocation(string id)
    {
        if (!int.TryParse(id, out var parsedId))
            return null;

        return _locations.FirstOrDefault(loc => loc.Id == parsedId);
    }

    public bool UpdateLocation(Location location)
    {
        var existing = _locations.FirstOrDefault(loc => loc.Id == location.Id);

        if (existing == null)
            return false; // No se encontró la ubicación

        // Actualizamos los campos con los valores del parámetro
        existing.Name = location.Name;
        existing.SpaceType = location.SpaceType;
        existing.Description = location.Description;

        return true; // Actualización exitosa
    }

    public bool CreateLocation(Location location)
    {
        // Simula la creación de una ubicación en base de datos
        location.Id = _locations.Max(loc => loc.Id) + 1; // Asigna un nuevo Id
        _locations.Add(location);
        return true;
    }
}