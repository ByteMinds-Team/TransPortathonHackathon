using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence.Repositories.EntityFramework;

public class TransportRequestRepository : EfRepositoryBase<TransportRequest, BaseDbContext>, ITransportRequestRepository
{
    public TransportRequestRepository(BaseDbContext context) : base(context)
    {
    }
}