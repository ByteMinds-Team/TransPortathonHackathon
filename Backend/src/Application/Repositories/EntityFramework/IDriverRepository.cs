using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.EntityFramework
{
    public interface IDriverRepository : IRepository<Driver>, IAsyncRepository<Driver>
    {
    }
}
