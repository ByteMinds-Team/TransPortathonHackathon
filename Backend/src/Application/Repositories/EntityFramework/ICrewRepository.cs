using Domain;

namespace Application.Repositories.EntityFramework;

public interface ICrewRepository : IRepository<Crew> , IAsyncRepository<Crew>
{
    
}