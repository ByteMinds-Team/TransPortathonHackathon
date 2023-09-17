using System.Linq.Expressions;
using Application.Common.Paging;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Repositories.EntityFramework;

public interface IAsyncRepository<T> : IQuery<T> where T : Entity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<IPaginate<T>> GetListByDynamicAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                                      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy =
                                                          null,
                                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>?
                                                          include = null,
                                                      bool enableTracking = true,
                                                      CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}