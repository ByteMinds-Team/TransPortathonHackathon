using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Common.Paging;
using Domain.Entities.Common;

namespace Application.Repositories.EntityFramework;

public interface IRepository<T> : IQuery<T> where T : Entity
{
    T Get(Expression<Func<T, bool>> predicate);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10,
        bool enableTracking = true);

    IPaginate<T> GetListByDynamic(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true);

    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
}