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
    public class ReviewRepository : EfRepositoryBase<Review, BaseDbContext>, IReviewRepository
    {
        public ReviewRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
