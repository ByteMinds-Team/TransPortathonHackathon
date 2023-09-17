using Domain.Entities;

namespace Application.Repositories.EntityFramework;

public interface IUserRepository:IRepository<User>,IAsyncRepository<User>
{
}
