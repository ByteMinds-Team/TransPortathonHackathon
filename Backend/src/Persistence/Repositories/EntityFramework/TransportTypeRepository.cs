using Application;
using Domain;
using Persistence.Repositories.EntityFramework;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence;

public class TransportTypeRepository : EfRepositoryBase<TransportType, BaseDbContext>, ITransportTypeRepository
{
    public TransportTypeRepository(BaseDbContext context) : base(context)
    {
    }
}
