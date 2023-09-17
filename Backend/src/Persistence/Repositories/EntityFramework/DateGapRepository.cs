using Application.Repositories.EntityFramework;
using Domain;
using Persistence.Repositories.EntityFramework.Contexts;

namespace Persistence.Repositories.EntityFramework;

public class DateGapRepository : EfRepositoryBase<DateGap, BaseDbContext>, IDateGapRepository
{
    public DateGapRepository(BaseDbContext context) : base(context)
    {
    }
}