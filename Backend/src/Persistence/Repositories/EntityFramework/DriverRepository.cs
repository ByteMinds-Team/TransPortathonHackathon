using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.EntityFramework
{
    public class DriverRepository : EfRepositoryBase<Driver, BaseDbContext>, IDriverRepository
    {
        public DriverRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
