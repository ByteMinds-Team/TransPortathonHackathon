using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence.Repositories.EntityFramework;

public class EmployeeRepository : EfRepositoryBase<Employee,BaseDbContext>, IEmployeeRepository
{
    public EmployeeRepository(BaseDbContext context) : base(context)
    {
    }
}