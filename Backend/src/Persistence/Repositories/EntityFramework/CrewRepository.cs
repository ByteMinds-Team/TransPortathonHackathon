using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence.Repositories.EntityFramework;

public class CrewRepository : EfRepositoryBase<Crew,BaseDbContext> , ICrewRepository
{
    public CrewRepository(BaseDbContext context) : base(context)
    {
    }
}