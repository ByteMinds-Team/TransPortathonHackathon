using Domain;

namespace Application.Repositories.EntityFramework;

public interface IOfferRepository : IRepository<Offer> , IAsyncRepository<Offer>
{
    
}