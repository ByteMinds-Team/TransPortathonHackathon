using System.Linq.Expressions;

namespace Application.Repositories.MongoDB;

public interface IAsyncMongoRepository<TEntity>
{
    Task AddAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
}
