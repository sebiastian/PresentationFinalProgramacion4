using Domain.Abstraction;
using Domain.Entity;

namespace Infrastructure.Persistence.Repository;

public class LocationRepository : RepositoryBase<Location>, ILocationRepository
{
    public LocationRepository(UserManagerDbContext context) : base(context)
    {
    }

}