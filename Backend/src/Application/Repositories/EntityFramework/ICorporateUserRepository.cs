using Domain;

namespace Application.Repositories.EntityFramework
{
    public interface ICorporateUserRepository : IRepository<CorporateCustomer>, IAsyncRepository<CorporateCustomer>
    {
    }
}
