using Application.Repositories.EntityFramework;
using Domain;

namespace Application;

public interface IVehicleRepository : IRepository<Vehicle>, IAsyncRepository<Vehicle>
{

}
