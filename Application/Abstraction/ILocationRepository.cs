using Domain.Entity;

namespace Application.Abstraction;

public interface ILocationRepository
{
    List<Location> GetAllLocation();
    Location GetLocation(string id);

    bool UpdateLocation(Location location);

    bool DeleteLocation(int id);
    bool CreateLocation(Location location);

}