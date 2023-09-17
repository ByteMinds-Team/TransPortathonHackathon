using System.Linq.Expressions;

namespace Application.Repositories.MongoDB;

public interface IMongoRepository<TEntity>
{
    void Add(TEntity entity);
    void Delete(int id);
    void Update(TEntity entity);
    TEntity Get(Expression<Func<TEntity, bool>> filter);
    IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);
}
