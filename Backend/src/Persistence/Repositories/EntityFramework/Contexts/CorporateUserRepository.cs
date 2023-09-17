using Application.Repositories.EntityFramework;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.EntityFramework.Contexts
{
    public class CorporateUserRepository : EfRepositoryBase<CorporateCustomer, BaseDbContext>, ICorporateUserRepository
    {
        public CorporateUserRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
