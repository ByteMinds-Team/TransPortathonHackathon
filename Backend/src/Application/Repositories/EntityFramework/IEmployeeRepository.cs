using Domain;

namespace Application.Repositories.EntityFramework;

public interface IEmployeeRepository : IRepository<Employee>, IAsyncRepository<Employee>
{
    
}