using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence.Repositories.EntityFramework;

public class OfferRepository : EfRepositoryBase<Offer,BaseDbContext> , IOfferRepository
{
    public OfferRepository(BaseDbContext context) : base(context)
    {
    }
}