using Application;
using Domain;
using Persistence.Repositories.EntityFramework;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence;

public class VehicleRepository : EfRepositoryBase<Vehicle, BaseDbContext>, IVehicleRepository
{
    public VehicleRepository(BaseDbContext context) : base(context)
    {
    }
}
