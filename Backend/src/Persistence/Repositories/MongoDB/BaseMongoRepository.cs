using Application.Repositories.MongoDB;
using Domain.Entities.Common;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Persistence.Repositories.MongoDB;

public class BaseMongoRepository<TEntity> : IMongoRepository<TEntity>, IAsyncMongoRepository<TEntity> where TEntity : Entity
{
    private readonly IMongoCollection<TEntity> _entityCollection;

    public BaseMongoRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _entityCollection = mongoDatabase.GetCollection<TEntity>(collectionName);
    }

    public void Add(TEntity entity)
    {
        _entityCollection.InsertOne(entity);
    }

    public void Delete(int id)
    {
        _entityCollection.DeleteOne(_ => _.Id == id);
    }

    public void Update(TEntity entity)
    {
        _entityCollection.ReplaceOne(x => x.Id == entity.Id, entity);
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        return _entityCollection.Find(filter).FirstOrDefault();
    }

    public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        return _entityCollection.Find(filter).ToList();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _entityCollection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _entityCollection.DeleteOneAsync(_ => _.Id == id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _entityCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        var data = await _entityCollection.FindAsync(filter);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        var fullCollection = await _entityCollection.FindAsync(filter);
        return await fullCollection.ToListAsync();
    }
}
